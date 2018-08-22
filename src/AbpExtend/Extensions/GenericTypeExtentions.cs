using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Collections.Extensions;

namespace Abp.Extensions
{
    /// <summary>
    /// 泛型类扩展
    /// </summary>
    public static class GenericTypeExtentions
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="params"></param>
        public static void ValidateParams<T>(this T @params) where T : class
        {
            var type = typeof(T);

            var properties = TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(@params)
                {
                    DisplayName = property.DisplayName,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(@params), validationContext);
                    if (result != null)
                    {
                        throw new ValidationException(result.ErrorMessage);
                    }
                }
            }

            if (@params is IValidatableObject)
            {
                var results = (@params as IValidatableObject).Validate(new ValidationContext(@params));

                var validationResults = results as ValidationResult[] ?? results.ToArray();
                if (validationResults.Any())
                {
                    throw new ValidationException(validationResults.First().ErrorMessage);
                }
            }


        }
    }
}