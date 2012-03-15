<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% foreach(var value in Enum.GetValues(typeof(DayOfWeek))) { %>
     <% var name = Enum.GetName(typeof(DayOfWeek), value); %>
     <label for="dayofweek<%=value %>"><%=name %></label>
     <input type="checkbox" id="dayofweek<%=value %>" name="dayofweek" value="<%=value %>" />
<% } %>