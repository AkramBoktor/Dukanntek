using Dukkantek.Domain.Entities;
using Dukkantek.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dukkantek.service.Helper
{
    public class Checker : IChecker
    {

        public void CheckBaseEntity(BaseEntity baseEntity, bool isEdit, string userId)
        {
            if (isEdit)
            {
                baseEntity.LastModifiedBy = userId;
                baseEntity.LastModifiedDate = DateTime.Now;
            }
            else
            {
                baseEntity.CreateBy = userId;
                baseEntity.CreateDate = DateTime.Now;
                baseEntity.IsDeleted = false;
            }
        }


        public bool IsEmailValid(string email)
        {
            string pattern = @"^\s*[\w\-\+_']+(\.[\w\-\+_']+)*\@[A-Za-z0-9]([\w\.-]*[A-Za-z0-9])?\.[A-Za-z][A-Za-z\.]*[A-Za-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}

