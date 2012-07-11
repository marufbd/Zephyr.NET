#Project Overview

A Framework project composed of two class library project Zephyr and Zephyr.Wev.Mvc and a sample ASP.NET MVC 3 sample application DemoApp.Web demonstrating how we can write an application using the framework by only creating our domain model and make CRUD operations on objects using the framework provided generic ZephyrCRUDController or do custom transaction using generic repository through overrides.

Framework features
===================
* Convention-based auto mapping for domain models
* Extension points for overriding default conventions for mapping(primary key, property type, table strategy)
* Generic repository for domain model CRUD operations and query with Specification(Spec) and Linq
* Unit of Work scope for business transactions
* ASP.NET MVC Html helpers (dropdown for enum, pager, flash message etc)
* Generic 360 views(List, Create, Update) for models
* Twitter bootstrap css for default view templates


The demo web app built on the framework can be seen running at:
http://defaultddd.apphb.com

References
=================
* As it goes with the same concept, some of the codes(Base Entity, Check class for Design by contract) are taken from [sharparchitecture](https://github.com/sharparchitecture/Sharp-Architecture)
* For NHibernate unit of work implementation see [this nhforge wiki](http://nhforge.org/wikis/patternsandpractices/nhibernate-and-the-unit-of-work-pattern.aspx)