﻿using MediatR;
using X10sions.Fake.Features.ToDo.Item;

namespace CleanOnionExample.Data.Entities.Services;

public class ToDoItemService : IToDoItemService {
  private readonly IToDoItemRepository _repository;
  private readonly IToDoItemFactory _factory;
  private readonly ToDoItemViewModelMapper _viewModelMapper;
  private readonly IMediator _mediator;

  public ToDoItemService(IToDoItemRepository repository, ToDoItemViewModelMapper viewModelMapper, IToDoItemFactory factory, IMediator mediator) {
    _repository = repository;
    _viewModelMapper = viewModelMapper;
    _factory = factory;
    _mediator = mediator;
  }

  public async Task<ToDoItemViewModel> Create(ToDoItemViewModel viewModel) {
    var newCommand = _viewModelMapper.ConvertToNewToDoItemCommand(viewModel);
    var result = await _mediator.Send((IRequest<ToDoItem>)newCommand);
    return _viewModelMapper.ConstructFromEntity(result);
  }

  public async Task Delete(Guid id) {
    var deleteCommand = _viewModelMapper.ConvertToDeleteToDoItemCommand(id);
    await _mediator.Publish(deleteCommand);
  }

  public async Task<IEnumerable<ToDoItemViewModel>> GetAllAsync(CancellationToken cancellationToken = default) {
    var entities = await _repository.GetAllAsync(x=> true, cancellationToken);
    return _viewModelMapper.ConstructFromListOfEntities(entities);
  }

  public async Task<ToDoItemViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
    var entity = await _repository.GetByPrimaryKeyAsync(new ToDoItemId(id), cancellationToken);
    return _viewModelMapper.ConstructFromEntity(entity);
  }
}