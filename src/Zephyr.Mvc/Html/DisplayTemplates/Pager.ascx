<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[Zephyr.Data.Models.IPagedList,Zephyr]]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>


<div class="pagination pagination-right">
  <ul>
    <% if (Model.IsFirstPage){ %>  
    <li class="disabled">
    <a href="#"> ← Previous </a>
    <% } %>
    <% else{ %>
    <li>
    <%= Html.ActionLink("← Previous", "List", new { page = Model.PageNumber - 1, items = Model.PageSize })%>
    <% } %>     
    </li>
            
    
    <% for (int i = 1; i <= Model.PageCount; i++) { %>        

    <% if (Model.PageNumber == i)
       { %>  
    <li class="active">
    <% } %>
    <% else
       { %>  
    <li>
    <% } %>        
    
    <% if (i==1 || i==Model.PageCount || (i > (Model.PageNumber - 3) && i < (Model.PageNumber + 3)) )
       { %>
    <%= Html.ActionLink(Convert.ToString(i), "List", new {page=i, items=Model.PageSize}) %>
    <% } %>
    <% else if ((Model.PageNumber > 4 && i == 2) || (Model.PageNumber < (Model.PageCount-3) && i == Model.PageCount - 1) )
       { %>
    <a href="#">...</a>
    <% } %>
     
    </li>
    
    <% } %>
    

    
    <% if (Model.IsLastPage){ %>  
    <li class="disabled">
    <a href="#">Newer → </a>
    <% } %>
    <% else{ %>        
    <li>
    <%= Html.ActionLink("Next →", "List", new { page = Model.PageNumber + 1, items = Model.PageSize })%>
    <% } %>     
    </li>
  </ul>
</div>