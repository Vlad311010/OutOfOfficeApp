using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using OutOfOfficeWebApp.Utils;
using System;

namespace OutOfOfficeWebApp.TagHelpers
{
    [HtmlTargetElement("modelinput", Attributes = "field, value, editable")]
    public class ModelInputTagHelper : TagHelper
    {
        [HtmlAttributeName("model")]
        public string ModelName { get; set; }

        [HtmlAttributeName("field")]
        public string FieldName { get; set; }
        
        [HtmlAttributeName("value")]
        public string FieldValue { get; set; }
        
        [HtmlAttributeName("editable")]
        public bool Editable { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            FieldValue = FieldValue.TrimStart().TrimEnd();

            output.TagName = @"div class=""mb-3""";

            string forValue = ModelName + "_" + FieldName;
            output.Content.AppendHtml(
                    string.Format(@"<label for=""{0}"" class=""form-label"">{1}</label>", forValue, FieldName.SplitCamelCase())
                );
            output.Content.AppendHtml(
                string.Format(@"<input id=""{0}"" name=""{1}"" type=""text"" class=""form-control"" value=""{2}"" {3}>",
                    forValue,
                    ModelName + "." + FieldName,
                    FieldValue,
                    Editable ? "" : "readonly"
                ));
            output.Content.AppendHtml(
                    string.Format(@"<span data-valmsg-for=""{0}"" data-valmsg-replace=""true"" class=""text-danger field-validation-valid"">", ModelName + "." + FieldName)
                );

            output.Attributes.Clear();
        }
    }
}
