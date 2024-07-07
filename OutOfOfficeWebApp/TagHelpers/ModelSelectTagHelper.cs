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
        
        [HtmlAttributeName("values")]
        public IEnumerable<SelectListItem> FieldValue { get; set; } = new List<SelectListItem>();
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = @"div class=""form-group""";

            string forValue = ModelName + "_" + FieldName;

            output.Content.AppendFormat(@"<label class=""control-label"" for=""{0}"">{1}</label>", forValue, FieldName.SplitCamelCase().Replace("Id", ""));
            TagHelperContent select = output.Content.AppendFormat(@"<select data-val=""true"" data-val-required=""The {0} field is required."" id=""{1}"" name=""{2}"" class=""form-control"">", 
                FieldName.SplitCamelCase(),
                forValue,
                ModelName + "." + FieldName
            );
            foreach (SelectListItem item in FieldValue)
            {
                select.AppendFormat(@"<option {0} value=""{1}"">{2}</option>", item.Selected ? @"selected=""selected""" : "", item.Value, item.Text);
            }
            output.Content.AppendHtml("</select>");
            output.Content.AppendFormat(@"<span class=""text-danger field-validation-valid"" data-valmsg-for=""{0}"" data-valmsg-replace=""true""></span>", ModelName + "." + FieldName);
            
            output.Attributes.Clear();
        }
    }
}
