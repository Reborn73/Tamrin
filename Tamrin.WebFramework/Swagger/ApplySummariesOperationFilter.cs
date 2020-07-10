using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Pluralize.NET;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using Tamrin.Common.Utilities;

namespace Tamrin.WebFramework.Swagger
{
    public class ApplySummariesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return;

            var pluralizer = new Pluralizer();

            var actionName = controllerActionDescriptor.ActionName;
            var singularizinName = pluralizer.Singularize(controllerActionDescriptor.ControllerName);
            var pluralizeName = pluralizer.Pluralize(singularizinName);

            var parameterCount = operation.Parameters.Count(p => p.Name != "version" && p.Name != "api-version");

            if (IsGetAllAction())
            {
                if (!operation.Summary.HasValue())
                    operation.Summary = $"Returns all {pluralizeName}";
            }
            else if (IsActionName("Post", "Create"))
            {
                if (!operation.Summary.HasValue())
                    operation.Summary = $"Creates a {singularizinName}";

                if (!operation.Parameters[0].Description.HasValue())
                    operation.Parameters[0].Description = $"A {singularizinName} representation";
            }
            else if (IsActionName("Read", "Get"))
            {
                if (!operation.Summary.HasValue())
                    operation.Summary = $"Retrieves a {singularizinName} by unique id";

                if (!operation.Parameters[0].Description.HasValue())
                    operation.Parameters[0].Description = $"a unique id for the {singularizinName}";
            }
            else if (IsActionName("Put", "Edit", "Update"))
            {
                if (!operation.Summary.HasValue())
                    operation.Summary = $"Updates a {singularizinName} by unique id";

                //if (!operation.Parameters[0].Description.HasValue())
                //    operation.Parameters[0].Description = $"A unique id for the {singularizeName}";

                if (!operation.Parameters[0].Description.HasValue())
                    operation.Parameters[0].Description = $"A {singularizinName} representation";
            }
            else if (IsActionName("Delete", "Remove"))
            {
                if (!operation.Summary.HasValue())
                    operation.Summary = $"Deletes a {singularizinName} by unique id";

                if (!operation.Parameters[0].Description.HasValue())
                    operation.Parameters[0].Description = $"A unique id for the {singularizinName}";
            }

            #region Local Functions
            bool IsGetAllAction()
            {
                foreach (var name in new[] { "Get", "Read", "Select" })
                {
                    if ((actionName.Equals(name, StringComparison.OrdinalIgnoreCase) && parameterCount == 0) ||
                        actionName.Equals($"{name}All", StringComparison.OrdinalIgnoreCase) ||
                        actionName.Equals($"{name}{pluralizeName}", StringComparison.OrdinalIgnoreCase) ||
                        actionName.Equals($"{name}All{singularizinName}", StringComparison.OrdinalIgnoreCase) ||
                        actionName.Equals($"{name}All{pluralizeName}", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }

            bool IsActionName(params string[] names)
            {
                foreach (var name in names)
                {
                    if (actionName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                        actionName.Equals($"{name}ById", StringComparison.OrdinalIgnoreCase) ||
                        actionName.Equals($"{name}{singularizinName}", StringComparison.OrdinalIgnoreCase) ||
                        actionName.Equals($"{name}{singularizinName}ById", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
            #endregion
        }
    }
}
