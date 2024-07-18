﻿using CleanOnionExample.Data.Entities.Services;
using X10sions.Fake.Domain.Services;
using X10sions.Fake.Features.Account;
using X10sions.Fake.Features.Owner;
using X10sions.Fake.Features.Person;
using X10sions.Fake.Features.WeatherForecast;

namespace CleanOnionExample.Services;

public sealed class ServiceManager : IServiceManager {
  private readonly Lazy<IAccountService2> _lazyAccountService2;
  private readonly Lazy<IOwnerService> _lazyOwnerService;
  private readonly Lazy<IPersonService> _lazyPersonService;

  public ServiceManager(IRepositoryManager repositoryManager) {
    _lazyAccountService2 = new Lazy<IAccountService2>(() => new AccountService2(repositoryManager.OwnerRepository, repositoryManager.AccountRepository));
    _lazyOwnerService = new Lazy<IOwnerService>(() => new OwnerService(repositoryManager.OwnerRepository));
    _lazyPersonService = new Lazy<IPersonService>(() => new PersonService(repositoryManager.PersonRepository));
  }

  public IAccountService2 AccountService2 => _lazyAccountService2.Value;
  public IOwnerService OwnerService => _lazyOwnerService.Value;
  public IPersonService PersonService => _lazyPersonService.Value;

}