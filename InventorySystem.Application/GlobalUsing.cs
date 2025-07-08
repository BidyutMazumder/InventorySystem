global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Identity;
global using AutoMapper;
global using System.Data;

global using InventorySystem.Application.ValidationBehavior;
global using InventorySystem.Application.Models;
global using InventorySystem.Application.Contracts.Infrastructure.Auth;
global using InventorySystem.Domain;
global using InventorySystem.Application.Features.Products.Commands.AddProduct;
global using InventorySystem.Application.Contracts.Persistence.Products;
global using InventorySystem.Application.Features.Products.Commands.UpdateProduct;
global using InventorySystem.Application.Features.Customers.Commands.AddCustomer;
global using InventorySystem.Application.Features.Products.Query.ProductList;
global using InventorySystem.Application.Contracts.Persistence.Customers;
global using InventorySystem.Application.Features.Customers.Commands.UpdateCustomer;
global using InventorySystem.Application.Features.Customers.Query.CustomerList;
global using InventorySystem.Domain.Products;
global using InventorySystem.Application.Contracts.Persistence.Sales;
global using InventorySystem.Domain.Sales;

