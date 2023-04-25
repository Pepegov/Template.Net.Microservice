#nullable enable
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace MicroserviceTemplate.Base.Helpers;

public class ClaimsHelper
{
    public static 
    #nullable disable
    IEnumerable<Claim> CreateClaims<T>(T entity, IEnumerable<Claim> additionalClaims = null) where T : class
    {
        if ((object) entity == null)
          throw new ArgumentNullException(nameof (entity));
        
        List<Claim> claims = new List<Claim>();
        if (additionalClaims != null)
          claims.AddRange(additionalClaims);
        
        IEnumerable<Claim> collection = ((IEnumerable<PropertyInfo>) typeof (T).GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (t => t.PropertyType.IsPrimitive || t.PropertyType.IsValueType || t.PropertyType == typeof (string))).Select(property => new
        {
          property = property,
          value = property.GetValue((object) (T) entity)
        }).Where(param => param.value != null).Select(param => new Claim(param.property.Name, param.value?.ToString()));
        
        claims.AddRange(collection);
        return (IEnumerable<Claim>) claims;
    }

    public static T GetValue<T>(ClaimsIdentity identity, string claimName)
    {
        Claim first = identity.FindFirst((Predicate<Claim>) (x => x.Type == claimName));
        if (first == null)
          return default (T);
        if (string.IsNullOrWhiteSpace(first.Value))
          return default (T);
        
        try
        {
          return (T) TypeDescriptor.GetConverter(typeof (T)).ConvertFromInvariantString(first.Value);
        }
        catch (Exception ex)
        {
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 3);
          interpolatedStringHandler.AppendFormatted(first.Value);
          interpolatedStringHandler.AppendLiteral(" from ");
          interpolatedStringHandler.AppendFormatted(first.Value);
          interpolatedStringHandler.AppendLiteral(" to ");
          interpolatedStringHandler.AppendFormatted<Type>(typeof (T));
          throw new InvalidCastException(interpolatedStringHandler.ToStringAndClear(), ex);
        }
    }

    public static List<T> GetValues<T>(ClaimsIdentity items, string claimName)
    {
        List<T> result = new List<T>();
        List<Claim> list = items.FindAll((Predicate<Claim>) (x => x.Type == claimName)).ToList<Claim>();
        if (!list.Any<Claim>())
          return result;
        
        list.ToList<Claim>().ForEach((Action<Claim>) (x =>
        {
          if (string.IsNullOrWhiteSpace(x.Value))
            return;
          
          try
          {
            result.Add((T) TypeDescriptor.GetConverter(typeof (T)).ConvertFromInvariantString(x.Value));
          }
          catch (Exception ex)
          {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 3);
            interpolatedStringHandler.AppendFormatted(x.Value);
            interpolatedStringHandler.AppendLiteral(" from ");
            interpolatedStringHandler.AppendFormatted(x.Value);
            interpolatedStringHandler.AppendLiteral(" to ");
            interpolatedStringHandler.AppendFormatted<Type>(typeof (T));
            
            throw new InvalidCastException(interpolatedStringHandler.ToStringAndClear(), ex);
          }
        }));
        
        return result;
    }

    private static Claim FindFirstOrEmpty(IEnumerable<Claim> claims, string claimType) 
      => claims.FirstOrDefault<Claim>((Func<Claim, bool>) (x => x.Value == claimType));
}


