<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[Zephyr.Data.Models.IPagedList,Zephyr]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>


<div class="pagination pagination-right">
  <ul>
    <% if (Model.IsFirstPage){ %>  
    <li class="disabled">
    <a href="#">« </a>
    <% } %>
    <% else{ %>
    <li>
    <%= Html.ActionLink("«", "List", new { page = Model.PageNumber - 1, items = Model.PageSize })%>
    <% } %>     
    </li>
    

    <% for (int i = 0; i < Model.PageCount; i++) { %>
    <% if (Model.PageNumber == i + 1)
       { %>  
    <li class="active">
    <% } %>
    <% else
       { %>  
    <li>
    <% } %>
    <%= Html.ActionLink(Convert.ToString(i+1), "List", new {page=i+1, items=Model.PageSize}) %>
    </li>
    <% } %>
    
    
    <% if (Model.IsLastPage){ %>  
    <li class="disabled">
    <a href="#"> » </a>
    <% } %>
    <% else{ %>        
    <li>
    <%= Html.ActionLink("»", "List", new { page = Model.PageNumber + 1, items = Model.PageSize })%>
    <% } %>     
    </li>
  </ul>
</div>