using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OutOfOfficeWebApp.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System;

namespace OutOfOfficeWebApp.TagHelpers
{
    [HtmlTargetElement("modelselect", Attributes = "model, field, values")]
    public class ModelSelectTagHelper : TagHelper
    {
        [HtmlAttributeName("model")]
        public string ModelName { get; set; }

        [HtmlAttributeName("field")]
        public string FieldName { get; set; }


        [HtmlAttributeName("hidden")]
        public bool Hidden { get; set; } = false;

        [HtmlAttributeName("values")]
        public IEnumerable<SelectListItem> FieldValue { get; set; } = new List<SelectListItem>();
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = @"div class=""form-group""";

            string forValue = string.IsNullOrEmpty(ModelName) ? FieldName : ModelName + "_" + FieldName;
            string nameValue = string.IsNullOrEmpty(ModelName) ? FieldName : ModelName + "." + FieldName;

            if (!Hidden)
                output.Content.AppendFormat(@"<label class=""control-label"" for=""{0}"">{1}</label>", forValue, FieldName.SplitCamelCase().Replace("Id", ""));

            TagHelperContent select = output.Content.AppendFormat(@"<select {0} data-val=""true"" data-val-required=""The {1} field is required."" id=""{2}"" name=""{3}"" class=""form-control"">",
                Hidden ? "hidden" : "",
                FieldName.SplitCamelCase(),
                forValue,
                nameValue
            );
            foreach (SelectListItem item in FieldValue)
            {
                select.AppendFormat(@"<option {0} value=""{1}"">{2}</option>", item.Selected ? @"selected=""selected""" : "", item.Value, item.Text);
            }
            output.Content.AppendHtml("</select>");
            output.Content.AppendFormat(@"<span class=""text-danger field-validation-valid"" data-valmsg-for=""{0}"" data-valmsg-replace=""true""></span>", nameValue);
            
            output.Attributes.Clear();
        }
    }
}
