CrocLinks is a URL shortening application compising of a .net web front end, API and a SQL server Database back end.

Features include, pasting in a "long" url, "eating" that URL and providing a shorter alternative (normally the hostname + upto a dozen alpha numeric characters)
A user can then copy the result, open a new table and the link shall be resolved by CrocLinks and then automatically redirected to the original web page.


## -- SET UP -- ##

To set up your own hosted version of CrocLinks you will need the following installed or pre-prepared:

- Atleast .NET 5.0.402 or above
- Access to a MS-SQL database server with an account of atleast db_owner permissions (more details below)
- Firefox or Chrome web browser (IE is untested, and therefore not supported!)


1) Clone the latest master branch to a directory of your choice
2) Set up the database via the method of you choice
  a) Create your own database 
    - Create users and name the Db using your prefered names, 
    - Run LinkShortenerDbSchema.sql in the root of the CrocLinks.API folder to create the entities.
    - Update DefaultConnection key in appsettings.json file(s) with your custom connection string
  b) Use the database provided
    - LinkShortener.bak is a copy of the database including all entity, functions and user permissions. The connection string in appsettings does not need to be changed       if you use this.
3) Open a terminal/cmd at both project paths {YOURDIRECTORY}\src\CrocLinks\CrocLinks.API and YOURDIRECTORY}\src\CrocLinks\CrocLinks.UI.
    - Optionally host in IIS
4) Type "dotnet run" for each.
5) Open your browser and navigate to your configured hostname or https://localhost:6001
6) Start eating those links!

Note: CrocLinks.API documentation in the form of swagger can be found at https://localhost:5001/index.html

## -- Future Features -- ##

1) Add better URL validation before saving to database (check the site actually exists)
2) Fix issue with some duplication of LinkUsage data
3) Ability to create accounts and customise links with personal branding
4) Cool animation of croc actually eating the link
5) Optional ability of adding expiry times to shortened links
6) Customise link output to more readable format
7) Include some more detailed metrics on link activity (charts?)
8) Add capabilty for EntityFramework to automatically create the database upon first run
9) Randomise link generation as its quite predictable at the moment
10) Add some database clean up jobs - sql express doesnt support sql agent :(
