<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%

   var htmlAttributes = new UFC.Helpers.HtmlPropertiesAttribute();

   if(ViewData.ModelMetadata.AdditionalValues.ContainsKey("HtmlAttributes"))

       htmlAttributes = (UFC.Helpers.HtmlPropertiesAttribute)ViewData.ModelMetadata.AdditionalValues["HtmlAttributes"];   

 %>
     <span><%=Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue,htmlAttributes.HtmlAttributes()) %></span>


