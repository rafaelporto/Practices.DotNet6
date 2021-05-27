using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using MediatR;
using Practices.DotNet6.WebApi.ToDo;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder();

builder.Host.ConfigureServices((context, services) =>
	{
		services.AddSingleton<List<TodoItem>>();
		services.AddMediatR(typeof(TodoListHandler).Assembly);
		services.AddMediatR(typeof(TodoQueryHandler).Assembly);
		services.AddMediatR(typeof(TodoAddHandler).Assembly);
		services.AddMediatR(typeof(TodoDeleteHandler).Assembly);
	});

await using var app = builder.Build();

var mediator = app.Services.GetService<IMediator>();

app.MapGet("/todos", (Func<int?, int?,IEnumerable<TodoItem>>)((int? limit, int? skip)  => 
		mediator.Send(new TodoListQuery(limit ?? 10, skip ?? 0)).Result));

app.MapGet("/todos/{id:int}", (Func<int, TodoItem>)((int id) => mediator.Send(new TodoQuery(id)).Result));

app.MapPost("/todos", (Action<TodoItem>)((TodoItem model) => mediator.Send(new TodoAddCommand(model.Text))));

app.MapDelete("/todos/{id:int}", (Action<int>)((int id) => mediator.Send(new TodoDeleteCommand(id))));

await app.RunAsync();

