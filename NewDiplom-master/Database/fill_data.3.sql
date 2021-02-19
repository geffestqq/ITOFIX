USE [DDD]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [Surname], [Name], [Second_Name], [Employee_Number]) VALUES (1, N'Петров', N'Петр', N'Петрович', 1)
INSERT [dbo].[Employees] ([Id], [Surname], [Name], [Second_Name], [Employee_Number]) VALUES (2, N'Иванов', N'Иван', N'Иванович', 2)
INSERT [dbo].[Employees] ([Id], [Surname], [Name], [Second_Name], [Employee_Number]) VALUES (3, N'Сидоров', N'Сидор', N'Сидорович', 3)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[Positions] ON 

INSERT [dbo].[Positions] ([Id], [Position_name]) VALUES (1, N'Должность 1')
INSERT [dbo].[Positions] ([Id], [Position_name]) VALUES (2, N'Должность 2')
INSERT [dbo].[Positions] ([Id], [Position_name]) VALUES (3, N'Должность 3')
INSERT [dbo].[Positions] ([Id], [Position_name]) VALUES (4, N'Должность 4')
INSERT [dbo].[Positions] ([Id], [Position_name]) VALUES (5, N'Должность 5')
SET IDENTITY_INSERT [dbo].[Positions] OFF
SET IDENTITY_INSERT [dbo].[Plurality] ON 

INSERT [dbo].[Plurality] ([Id], [EmployeeId], [PositionId]) VALUES (1, 1, 1)
INSERT [dbo].[Plurality] ([Id], [EmployeeId], [PositionId]) VALUES (2, 1, 2)
INSERT [dbo].[Plurality] ([Id], [EmployeeId], [PositionId]) VALUES (3, 1, 3)
INSERT [dbo].[Plurality] ([Id], [EmployeeId], [PositionId]) VALUES (4, 2, 1)
INSERT [dbo].[Plurality] ([Id], [EmployeeId], [PositionId]) VALUES (5, 3, 2)
INSERT [dbo].[Plurality] ([Id], [EmployeeId], [PositionId]) VALUES (6, 3, 4)
INSERT [dbo].[Plurality] ([Id], [EmployeeId], [PositionId]) VALUES (7, 3, 5)
SET IDENTITY_INSERT [dbo].[Plurality] OFF
SET IDENTITY_INSERT [dbo].[Functions] ON 

INSERT [dbo].[Functions] ([Id], [Name_function]) VALUES (1, N'Функция ИС 1')
INSERT [dbo].[Functions] ([Id], [Name_function]) VALUES (2, N'Функция ИС 2')
INSERT [dbo].[Functions] ([Id], [Name_function]) VALUES (3, N'Функция ИС 3')
INSERT [dbo].[Functions] ([Id], [Name_function]) VALUES (4, N'Функция ИС 4')
INSERT [dbo].[Functions] ([Id], [Name_function]) VALUES (5, N'Функция ИС 5')
SET IDENTITY_INSERT [dbo].[Functions] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name_Role]) VALUES (1, N'Роль 1')
INSERT [dbo].[Roles] ([Id], [Name_Role]) VALUES (2, N'Роль 2')
INSERT [dbo].[Roles] ([Id], [Name_Role]) VALUES (3, N'Роль 3')
INSERT [dbo].[Roles] ([Id], [Name_Role]) VALUES (4, N'Роль 4')
INSERT [dbo].[Roles] ([Id], [Name_Role]) VALUES (5, N'Роль 5')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Rights] ON 

INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (1, 1, 1, 1)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (2, 2, 1, 2)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (3, 3, 1, 4)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (4, 4, 2, 1)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (5, 5, 2, 2)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (6, 6, 2, 5)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (7, 7, 3, 2)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (8, 8, 3, 4)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (9, 9, 4, 1)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (10, 10, 4, 2)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (11, 11, 5, 4)
INSERT [dbo].[Rights] ([Id], [Give_out], [RoleId], [FunctionId]) VALUES (12, 12, 5, 5)
SET IDENTITY_INSERT [dbo].[Rights] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [Status_name]) VALUES (1, N'Статус 1')
INSERT [dbo].[Statuses] ([Id], [Status_name]) VALUES (2, N'Статус 2')
INSERT [dbo].[Statuses] ([Id], [Status_name]) VALUES (3, N'Статус 3')
INSERT [dbo].[Statuses] ([Id], [Status_name]) VALUES (4, N'Статус 4')
INSERT [dbo].[Statuses] ([Id], [Status_name]) VALUES (5, N'Статус 5')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
SET IDENTITY_INSERT [dbo].[Zadachis] ON 

INSERT [dbo].[Zadachis] ([Id], [Task_Name], [Task_Detail], [Date_Open], [Date_Close], [ZadachiParentId], [StatusId]) VALUES (1, N'Задача 1', N'Задача 1 подробно', N'2020-01-01', N'2020-01-01', 1, 2)
INSERT [dbo].[Zadachis] ([Id], [Task_Name], [Task_Detail], [Date_Open], [Date_Close], [ZadachiParentId], [StatusId]) VALUES (2, N'Задача 2', N'Задача 2 подробно', N'2020-01-12', N'2020-03-12', NULL, 3)
INSERT [dbo].[Zadachis] ([Id], [Task_Name], [Task_Detail], [Date_Open], [Date_Close], [ZadachiParentId], [StatusId]) VALUES (3, N'Задача 3', N'Задача 3 подробно', N'2020-09-01', N'2020-12-01', 2, 5)
INSERT [dbo].[Zadachis] ([Id], [Task_Name], [Task_Detail], [Date_Open], [Date_Close], [ZadachiParentId], [StatusId]) VALUES (4, N'Задача 5', N'Задача 5 подробно', N'2020-07-01', N'2021-01-01', 2, 4)
INSERT [dbo].[Zadachis] ([Id], [Task_Name], [Task_Detail], [Date_Open], [Date_Close], [ZadachiParentId], [StatusId]) VALUES (5, N'Задача 7', N'Задача 7 подробно', N'2020-07-01', N'2020-03-12', 4, 3)
INSERT [dbo].[Zadachis] ([Id], [Task_Name], [Task_Detail], [Date_Open], [Date_Close], [ZadachiParentId], [StatusId]) VALUES (6, N'Задача 8', N'Задача 8 подробно', N'2020-07-01', N'2020-03-12', NULL, 4)
SET IDENTITY_INSERT [dbo].[Zadachis] OFF
SET IDENTITY_INSERT [dbo].[TaskDistributions] ON 

INSERT [dbo].[TaskDistributions] ([Id], [StartedAt], [PeriodUnit], [PeriodValue], [PluralityId], [ZadachiId], [StatusId]) VALUES (1, '2020-08-01 14:00', 0, 3, 1, 1, 2)
INSERT [dbo].[TaskDistributions] ([Id], [StartedAt], [PeriodUnit], [PeriodValue], [PluralityId], [ZadachiId], [StatusId]) VALUES (2, '2020-09-20 12:00', 2, 7, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[TaskDistributions] OFF
SET IDENTITY_INSERT [dbo].[TypeComments] ON 

INSERT [dbo].[TypeComments] ([Id], [Name_comment]) VALUES (1, N'Вид комментария 1')
INSERT [dbo].[TypeComments] ([Id], [Name_comment]) VALUES (2, N'Вид комментария 2')
INSERT [dbo].[TypeComments] ([Id], [Name_comment]) VALUES (3, N'Вид комментария 3')
INSERT [dbo].[TypeComments] ([Id], [Name_comment]) VALUES (4, N'Вид комментария 4')
INSERT [dbo].[TypeComments] ([Id], [Name_comment]) VALUES (5, N'Вид комментария 5')
SET IDENTITY_INSERT [dbo].[TypeComments] OFF

SET IDENTITY_INSERT [dbo].[FileTask] ON 

INSERT [dbo].[FileTask] ([Id], [Path_File], [File_Detail], [TaskDistributionId]) VALUES (1, N'Путь к файлу 1', N'Файл подробно 1', 1)
INSERT [dbo].[FileTask] ([Id], [Path_File], [File_Detail], [TaskDistributionId]) VALUES (2, N'Путь к файлу 2', N'Файл подробно 2', 1)
INSERT [dbo].[FileTask] ([Id], [Path_File], [File_Detail], [TaskDistributionId]) VALUES (3, N'Путь к файлу 3', N'Файл подробно 3', 1)
INSERT [dbo].[FileTask] ([Id], [Path_File], [File_Detail], [TaskDistributionId]) VALUES (4, N'Путь к файлу 4', N'Файл подробно 4', 1)
INSERT [dbo].[FileTask] ([Id], [Path_File], [File_Detail], [TaskDistributionId]) VALUES (5, N'Путь к файлу 5', N'Файл подробно 5', 1)
SET IDENTITY_INSERT [dbo].[FileTask] OFF

SET IDENTITY_INSERT [dbo].[TaskComments] ON 

INSERT [dbo].[TaskComments] ([Id], [Text_comment], [Type_CommentsId], [TaskDistributionsId]) VALUES (1, N'Комментарий 1', 1, 1)
INSERT [dbo].[TaskComments] ([Id], [Text_comment], [Type_CommentsId], [TaskDistributionsId]) VALUES (2, N'Комментарий 2', 3, 2)
INSERT [dbo].[TaskComments] ([Id], [Text_comment], [Type_CommentsId], [TaskDistributionsId]) VALUES (3, N'Комментарий 3', 4, 1)
SET IDENTITY_INSERT [dbo].[TaskComments] OFF
