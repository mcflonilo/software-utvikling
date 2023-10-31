using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Collections.Generic;

namespace ConsoleApp
{

    public class Program
    {
        static void Main(string[] args)
        {
            Education education = new() { Name = "Data Science"};
            addTeacher(new() { Name = "tomas", Teaches = education });
            getTeacherLINQ();


        }
        

        #region EFCore
        
        #endregion
    }
}