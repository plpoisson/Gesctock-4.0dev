Version 2.0
Added new features: Sql query for selected list
After upload new version of plugin, please run this query:
alter table [dbo].[SqlFilter]
add SqlQuery nvarchar(max)
GO
update [dbo].[SqlFilter] set SqlQuery = ''
GO