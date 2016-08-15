namespace MyData.Migrations
{
    using MyData.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyData.Domain.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyDbContext context)
        {
            //Test2(context);
        }

        private void Test1(MyDbContext context)
        {
            var pr1 = CreateProject(context, "Project 1", 2, 3);
            var pr2 = CreateProject(context, "Project 2", 3, 0);

            var iteration = new CIteration(new DateTime(2016, 1, 1), new DateTime(2016, 1, 31));
            pr1.Tasks.ToList().ForEach(x => iteration.Tasks.Add(x));
            pr2.Tasks.ToList().ForEach(x => iteration.Tasks.Add(x));
            context.Iterations.AddOrUpdate(iteration);
        }

        private void Test2(MyDbContext context)
        {
            var project = new CProject("Project");
            context.Projects.AddOrUpdate(project);
        }

        private CProject CreateProject(MyDbContext context, string name, int countTasks, int countProjects)
        {
            var project = new CProject(name);
            for (int i = 1; i <= countTasks; i++)
                project.Tasks.Add(new CTask("Task " + i));
            for (int i = 1; i <= countProjects; i++)
                project.Projects.Add(new CProject(name + "." + i));
            context.Projects.AddOrUpdate(project);
            return project;
        }
    }
}
