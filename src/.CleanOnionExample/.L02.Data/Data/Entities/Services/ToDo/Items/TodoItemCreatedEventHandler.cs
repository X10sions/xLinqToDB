﻿using Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using X10sions.Fake.Features.ToDo.Item;

namespace CleanOnionExample.Data.Entities.Services;

public class ToDoItemCreatedEventHandler : INotificationHandler<DomainEventNotification<ToDoItemCreatedEvent>> {
  private readonly ILogger<ToDoItemCreatedEventHandler> _logger;

  public ToDoItemCreatedEventHandler(ILogger<ToDoItemCreatedEventHandler> logger) {
    _logger = logger;
  }

  public Task Handle(DomainEventNotification<ToDoItemCreatedEvent> notification, CancellationToken cancellationToken) {
    var domainEvent = notification.DomainEvent;
    _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);
    return Task.CompletedTask;
  }

}
