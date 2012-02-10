<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[[Zephyr.Web.Mvc.Html.Models.TabHelperModel,Zephyr.Web.Mvc]]" %>

<ul class="tabs" data-tabs="tabs">
<% foreach (var item in Model) { %>
    <% if (item.Active)
       {%>
    <li class="active"><a href="#<%: item.Title %>"><%: item.Title %></a></li>
    <% } %>
    <% else { %>
    <li><a href="#<%: item.Title %>"><%: item.Title %></a></li>
    <% } %>
<% } %>
</ul>
<div class="tab-content">
<% foreach (var item in Model) { %>
    <% if (item.Active)
       {%>
    <div class="tab-pane active" id="<%: item.Title %>">
        <p><%: item.HtmlContent %></p>
    </div>    
    <% } %>
    <% else { %>
    <div class="tab-pane" id="<%: item.Title %>">
        <p><%: item.HtmlContent %></p>
    </div>
    <% } %>
<% } %>
</div>