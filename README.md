# CloudSharp
[![Build Status](https://travis-ci.org/lewisgcm/CloudSharp.svg?branch=master)](https://travis-ci.org/lewisgcm/CloudSharp)
[![License Type](https://img.shields.io/badge/license-GPL%203.0-blue.svg)](https://www.gnu.org/licenses/gpl-3.0.en.html)
[![NuGet](https://badge.fury.io/nu/CloudSharp.svg)](https://www.nuget.org/packages/cloudsharp/)

C# Library providing helper classes and patterns to .NET Core.

# Whats the big idea?
This library aims at providing a simple starting point for quickly building REST driven microservices.
Repositories have various implementations such as *DatabaseRepository*, *CachedDatabaseRepository*, *MicroserviceRepository* 
and *CachedMicroserviceRepository*.

Each repository implementation implements the *IRepository<ID, Model>* interface providing async 
CRUD opertions on the specified model type.
This means you the programmer, can freely swap out implementations as part of the dependency injection configuration.

The *IService* interface also exposes async CRUD opertions and is used as part of the generic *Controller* class.
*Service* is a generic implementation of *IService* simplly wrapping the injected *IRepository* implementation.