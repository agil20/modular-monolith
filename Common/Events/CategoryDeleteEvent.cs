using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events
{
    public class CategoryDeleteEvent:INotification
    {
        public int CategoryId { get; set; }

        public CategoryDeleteEvent(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
