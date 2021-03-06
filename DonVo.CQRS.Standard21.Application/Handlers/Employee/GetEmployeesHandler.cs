﻿using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Application.ViewModels.Employee;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeLiteViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;

        public GetEmployeesHandler(IMediator mediator, IEmployeeRepository employeerepository)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
        }

        public async Task<IEnumerable<EmployeeLiteViewModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await EmployeeRepository.Select();
            var viewmodel = new List<EmployeeLiteViewModel>();
            foreach (var item in employees)
            {
                var vm = new EmployeeLiteViewModel();
                vm.LoadFromDomain(item);
                viewmodel.Add(vm);
            }
            return viewmodel.OrderBy(item => item.LastName).ThenBy(item => item.FirstName).ToList();
        }
    }
}