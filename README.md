
To run the project You'll need to:

1. Install "Visual Studio 2015" open it in "Admin" mode, then open the "WaveWareTimes.sln"-file 
2. In "WaveWareTimes.Web"-project's "Web.config"-file set the SQL "Server=", "User ID=" and "Password=" -> like below (it should be written in one line):

<add name="IdentityDbConnection" connectionString="Server=(localdb)\v12.0;Database=WaveWareTimes;User ID=sa;Password=pass;Trusted_Connection=False;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=True;" providerName="System.Data.SqlClient" />

 
3. Right-click on "WaveWareTimes.Web"-project and "Set as StartUp Project"
4. Open "Package Manager Console"-window in "Visual Studio 2015" ("View"->"Other Windows"->"Package Manager Console")
5. Select the "Default project:"->"WaveWareTimes.Infrastructure.Data" and run the folowing command > Update-Database -Verbose
6. After the command run, EntityFramework will create:
      Database -> WaveWareTimes
      Supervisor -> UserName=test_admin, Password=Test123#, Role=Admin
      Simple user -> UserName=test_user, Password=Test123#, Role=User
      WorkTimeRecord -> for "test_user"
7. Run the project by clicking F5
8. Login with credentials for UserName=test_admin, Password=Test123#
- As a Supervisor "test_admin" will be able to register new users and see the list of all registered users
- All users can sign in with the credentials specified by the Supervisor on registration page
- Each user can see the WorkTimeRecords created by them
- Supervisor can see all records created by all users and can manage these records (group them by User column, etc.)