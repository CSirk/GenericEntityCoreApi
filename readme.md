# Generic Repository Pattern with EF Core

Note that .Net Core 3.x has issues with EF Core using MySQL connector, so project uses .NET Core 2.2
**See .csproj files for working references**


0. Ensure that the mysql connector is intalled

1. Create new solution with a project for the api and name it "SolutionName.Api"
	Type: Asp.net Core Web Application
		- Api
			- .Net Core 2.2
			
2. Create new Project in the solution of type ".Net Core class library" and name it "SolutionName.EntityFramework"
	- Right click the project and say "edit project file"
		- Change the .Net Core Version from x.x to 2.2
		
3. Repeat step 2 for a new project "SolutionName.Core"

4. Add in references to/in below:
	MySql.Data.EntityFrameworkCore 8.0.19 / .Api, .EntityFramework
	Microsoft.EntityFrameworkCore 2.2.6 / .Api / .EntityFramework
	Microsoft.EntityFrameworkCore.Design 2.2.6 / .Api / .EntityFramework
	Microsoft.EntityFrameworkCore.Tools 2.2.6 / .Api / .EntityFramework
	Optional : (if you want swagger page) Swashbuckle.AspNetCore 4.01 / .Api
			
5a. (Optional, set up Swagger):
	- In Api project 
		- expand "Properties" and open "launchSettings.json"
		Change "launchUrl" to "swagger" in profiles, and .Api nodes
		- In Startup.cs
			- in "Configure()"
				- add the following lines above "app.UseHttpsRedirection();
					app.UseSwagger();

					app.UseSwaggerUI(c =>
					{
						c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
					});
			- in "ConfigureServices()"
				- add the following lines at the top of the method:
					services.AddSwaggerGen(c =>
					{
						c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
					});
		
		
5. Create your MySQL database and add one or more tables, for the inital set up to test your connection and make the generic repository to use for CRUD operations avoid foreign keys. Keep the table simple until you know the connections and setup work.

6. Add a reference to the EntityFramework project in your API project

7. Open the "Package Manager Console" window and target your EntityFramework Project in the "Default Project" dropdown
	- Run the following command (replacing ** with your own properties)
		- Scaffold-DbContext "server=**;port=**;user=**;password=**;database=**" MySql.Data.EntityFrameworkCore -OutputDir ** -f
	- In the Created Context.cs file
		- Copy the line below from the file (it will contain your own values) and then remove the warning and the line you copied from the Context file.
			optionsBuilder.UseMySQL("");
	- In Startup.cs in the ConfigureServices()
		- Add the following line (replace with your copied string from the Context file.)
            services.AddDbContext<*yourContextName*>(options => options.UseMySQL(""));
			
8. Create a new folder in your ".Core" project and name it "Models"
			- Create a model class to match your generated entity classes for each table in your database entity framework generated during step 7
				- You can copy and paste all properties from the generated entity classes into your model classes, it is good practice to name the domain classes (in core) differently than the names of the entities for simplicity. I named mine "Dom*NameOfTable*"
				
9. Create a folder in the .EntityFramework project called "Repositories"
	- Create a class file for each Entity Model named "*modelName*Repository"
	- Create a class file called "GenericRepository"
	- Create an interface file called "IGenericRepository"
	
10. Copy from this sample project the code exactly for IGenericRepository and GenericRepository, these files are the basis for this generic repository pattern

11. In each of your Repositories for each entity model (db table) copy the pattern in "CustomerRepository" from this example solution, replacing your context and the entity model type for your own

12. In Startup.cs add the following line of code (for dependency injection):
        services.AddTransient<IGenericRepository<*yourcontext*, *yourentitymodel*>, *yourrepository*>();
		
13. In your .Core project create the following folders and classes:
	- Interfaces: 
		- make an interface called "I*YourEntityModelName*Service
	- Services
		- make a class called "*YourEntityModelName*Service"
	- Extensions
		- make a class called "*YourEntityModelName*Extensions"
		
14. In the extensions class follow the pattern of "CustomerExtensions" in this repository, you will replace all references to Customer with your own Entity Names, Properties, and Contexts. The extension methods will allow the transformations of entity objects to domain objects and domain objects to entity objects in your api and database actions

15. In the Interfaces Folder of Core within your I*YourEntityModelName*Service*Service follow the pattern of "ICustomerService" in this repository. This will give your Service which implements this interface the database actions it needs.

16. In the Services Folder of Core within your *YourEntityModelName*Service*Service follow the pattern of "CustomerService" in this repository. This service connects to the instance of the generic repository allowing CRUD database operations on your entity. You will need to reference your Extensions class to get the domain to entity and entity to domain operations 

17. In Startup.cs add the following line under your repository declaration from step 12 (this is for dependency injection)
	services.AddTransient<I*YourServiceName*, *YourServiceName*>();
	
18. In your .Api project do the following:
	- Right click "controllers" and "add controller"
		- Choose "Api Controller - Empty" (optionally you can choose "Api Controller - With Actions using Entity Framework", this will give you CRUD operations, but we are going to create these on our own) and name it *yourentitymodel*Controller

19. Copy from CustomerController in this repository replacing references to customer or Customer with your own entity the controller serves, and properties of your entity with the ones in the "SearchCustomers" method

20. Click "Run" at the top of visual studio and if swagger was implemented in step 5a, your api should load showing your controller! Test out the methods and hope everything worked! :-D 

21. For each entity model you add in the future you will create services, interfaces for the services, extensions, and controllers, but the process remains the same! 