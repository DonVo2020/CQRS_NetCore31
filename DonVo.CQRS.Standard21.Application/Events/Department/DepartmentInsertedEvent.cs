﻿using MediatR;

namespace DonVo.CQRS.Standard21.Application.Events.Department
{
	public class DepartmentInsertedEvent : INotification
	{
		public int Id { get; set; }
	}
}