using E_Commerce.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace E_Commerce.Infrastructure
{
    public class SchemaExampleFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(Order))
            {
                schema.Example = GetOrderExample();
            }
            else if (context.Type == typeof(OrderDetail))
            {
                schema.Example = GetOrderDetailExample();
            }
            else if (context.Type == typeof(Address))
            {
                schema.Example = GetAddressExample();
            }
            else if (context.Type == typeof(Customer))
            {
                schema.Example = GetCustomerExample();
            }
            else if (context.Type == typeof(ShoppingCartItem))
            {
                schema.Example = GetShoppingCartItemExample();
            }
            else if (context.Type == typeof(Category))
            {
                schema.Example = GetCategoryExample();
            }
            else if (context.Type == typeof(Product))
            {
                schema.Example = GetProductExample();
            }
            else if (context.Type == typeof(ProductImage))
            {
                schema.Example = GetProductImageExample();
            }
        }

        private OpenApiObject GetOrderExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["orderDate"] = new OpenApiDateTime(DateTime.Now),
                ["customerId"] = new OpenApiInteger(1),
                ["addressId"] = new OpenApiInteger(1)
            };
        }
        private OpenApiObject GetOrderDetailExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["orderId"] = new OpenApiInteger(1),
                ["productId"] = new OpenApiInteger(1),
                ["quantity"] = new OpenApiInteger(1)
            };
        }

        private OpenApiObject GetAddressExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["street"] = new OpenApiString("123 Street Name"),
                ["city"] = new OpenApiString("City Name"),
                ["state"] = new OpenApiString("State Name"),
                ["zipCode"] = new OpenApiString("12345"),
                ["customerId"] = new OpenApiInteger(1)
            };
        }

        private OpenApiObject GetCustomerExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["firstName"] = new OpenApiString("John"),
                ["lastName"] = new OpenApiString("Doe"),
                ["email"] = new OpenApiString("john.doe@example.com")
            };
        }
        private OpenApiObject GetShoppingCartItemExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["customerId"] = new OpenApiInteger(1),
                ["productId"] = new OpenApiInteger(1),
                ["quantity"] = new OpenApiInteger(2)
            };
        }

        private OpenApiObject GetCategoryExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["name"] = new OpenApiString("Electronics")
            };
        }

        private OpenApiObject GetProductExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["name"] = new OpenApiString("Laptop"),
                ["description"] = new OpenApiString("A nice laptop"),
                ["price"] = new OpenApiDouble(999.99),
                ["categoryId"] = new OpenApiInteger(1)
            };
        }

        private OpenApiObject GetProductImageExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["imageUrl"] = new OpenApiString("https://example.com/images/laptop.jpg"),
                ["productId"] = new OpenApiInteger(1)
            };
        }
    }

}
