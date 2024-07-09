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

        [HtmlAttributeName("textarea")]
        public bool TextArea { get; set; } = false;

        [HtmlAttributeName("editable")]
        public bool Editable { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            FieldValue = FieldValue.TrimStart().TrimEnd();

            output.TagName = @"div class=""mb-3""";

            string forValue = string.IsNullOrEmpty(ModelName) ? FieldName : ModelName + "_" + FieldName;
            string nameValue = string.IsNullOrEmpty(ModelName) ? FieldName : ModelName + "." + FieldName;
            output.Content.AppendHtml(
                    string.Format(@"<label for=""{0}"" class=""form-label"">{1}</label>", forValue, FieldName.SplitCamelCase())
                );

            if (TextArea)
            {
                output.Content.AppendFormat(@"<textarea rows=7 id=""{0}"" name=""{1}"" type=""text"" class=""form-control"" {2}>{3}</textarea>",
                    forValue,
                    nameValue,
                    Editable ? "" : "readonly",
                    FieldValue
                );
            }
            else
            {
                output.Content.AppendFormat(@"<input id=""{0}"" name=""{1}"" type=""text"" class=""form-control"" value=""{2}"" {3}>",
                    forValue,
                    nameValue,
                    FieldValue,
                    Editable ? "" : "readonly"
                );
            }

            output.Attributes.Clear();
        }
    }
}
