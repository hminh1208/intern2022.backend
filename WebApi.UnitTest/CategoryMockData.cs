using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Enums;

namespace WebApi.UnitTest
{
    public static class CategoryMockData
    {
        public const int NUMBER_OF_APPROVED = 10;
        public const int NUMBER_OF_DELETED= 10;

        public static List<Category> GenerateCategoriesMockData(Account account)
        {
            var categories = new List<Category>();

            for(int i = 1; i <= NUMBER_OF_APPROVED; i++)
            {
                categories.Add(new Category()
                {
                    Name = Guid.NewGuid().ToString(),
                    ParentId = null,
                    Status = StatusEnum.APPROVED,
                    CreatedAccountId = account.Id,
                    UpdatedAccountId = account.Id,
                });
            }

            for (int i = 1; i <= NUMBER_OF_DELETED; i++)
            {
                categories.Add(new Category()
                {
                    Name = Guid.NewGuid().ToString(),
                    ParentId = null,
                    Status = StatusEnum.DELETED,
                    CreatedAccountId = account.Id,
                    UpdatedAccountId = account.Id,
                });
            }

            return categories;
        }
    }
}
