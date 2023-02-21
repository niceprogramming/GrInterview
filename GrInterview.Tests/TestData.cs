using GrInterview.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrInterview.Api;

namespace GrInterview.Tests
{
    internal static class TestData
    {
        public static string SuccessfulPostJson = """{"lastName":"User","firstName":"Test","email":"test.user@gmail.com","favoriteColor":"brown","dateOfBirth":"7/26/1994"}""";

        public static string SuccessfulTestOutput => $@" ---------------------------------------------------------------------------- 
 | LastName | FirstName | Email               | FavoriteColor | DateOfBirth |
 ---------------------------------------------------------------------------- 
 | Ted      | Test      | test.user@gmail.com | Apple         | 4/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sam      | Test      | test.user@gmail.com | brown         | 7/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sid      | Test      | test.user@gmail.com | green         | 1/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sadie    | Test      | test.user@gmail.com | orange        | 9/26/1994   |
 ---------------------------------------------------------------------------- 
 | Z        | Test      | test.user@gmail.com | yellow        | 8/26/1994   |
 ---------------------------------------------------------------------------- 

 ---------------------------------------------------------------------------- 
 | LastName | FirstName | Email               | FavoriteColor | DateOfBirth |
 ---------------------------------------------------------------------------- 
 | Sid      | Test      | test.user@gmail.com | green         | 1/26/1994   |
 ---------------------------------------------------------------------------- 
 | Ted      | Test      | test.user@gmail.com | Apple         | 4/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sam      | Test      | test.user@gmail.com | brown         | 7/26/1994   |
 ---------------------------------------------------------------------------- 
 | Z        | Test      | test.user@gmail.com | yellow        | 8/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sadie    | Test      | test.user@gmail.com | orange        | 9/26/1994   |
 ---------------------------------------------------------------------------- 

 ---------------------------------------------------------------------------- 
 | LastName | FirstName | Email               | FavoriteColor | DateOfBirth |
 ---------------------------------------------------------------------------- 
 | Z        | Test      | test.user@gmail.com | yellow        | 8/26/1994   |
 ---------------------------------------------------------------------------- 
 | Ted      | Test      | test.user@gmail.com | Apple         | 4/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sid      | Test      | test.user@gmail.com | green         | 1/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sam      | Test      | test.user@gmail.com | brown         | 7/26/1994   |
 ---------------------------------------------------------------------------- 
 | Sadie    | Test      | test.user@gmail.com | orange        | 9/26/1994   |
 ---------------------------------------------------------------------------- 

";
    }
}
