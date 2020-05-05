## Steps to create and initialize the database
	
	The application uses local database. If needed change the connection string in the appsettings.Development.json.
	The application used Code first design pattern so the database will be created and seeded with data on the first run of the application. 

## Steps to prepare the source code to build/run properly 

	Just Rebuild and Start the API. No authorization and authentication is implemented so you are able to send requests to the endpoints which are: 
	* api/category – GET -> returns all (predefined) categories
	* api/website/{id} – GET -> returns record by id
	* api/website?{queryFilter} – GET -> returns collection of records. Query filter could contain: 
		- Filter.Skip – integer how much records to skip – used for paging
		- Filter.Take – integer how much records to select – used for paging
		- Filter.SortDescriptor.Dir – string (‘Asc’ or ‘Desc’) used for sort direction. 
		- Filter.SortDescriptor.Field – string – by which field to sort the collection. Possible values are: 	
			* Name
			* URL
			* Category
			* UserEmail
	* api/website – POST -> adds website in the database if all validations passes. All fields are required and the name must be unique. 
		- CategoryCode must be one of the predefined codes ('SPORT', 'EDU', 'TECH', 'NEWS' or 'ENT')
		- Example JSON: 
			{
				"Name":  "Football",
				"URL": "http://footballURL.com",
				"Login": {
					"Email": "test@test.bg",
					"Password": "Test123!"
				},
				"CategoryCode": "SPORT",
				"HomepageSnapshot": "////////////////////////////////////////////////////////////////////////////gf////////////////////////f/z///4+fH///ngef//+eB5///74H3////wf/////D//////////////////n/H///+H4f///4fh////w8P////n5/////AP////////////////////////////////////////////////////////////////////8="
			}
	* api/website/{id} – PATCH -> Updates website for the provided Id. In the current version, it is not possible to update the Login of a website. It could be implemented via extra query dedicated only for updating the login. 
	* api/website/{id} – DELETE -> Soft deletes record by its id. 

## Any assumptions made and missing requirements that are not covered in the specifications 

	1. The name of the website should be unique in the NON-deleted websites. 
	2. The password should have some restrictions - in this version the only restriction is to be at least 6 symbols. 
	3. The password must be hashed and the application uses simple hash algorithm with salt for each user. 
	4. The Login for each website is a unique user. Database design allows that one user could have multiple sites but the API endpoint does not work like this. 
	5. The GET requests returns only NON-deleted websites. 
	6. The image should be Base64 string, which is converted to byte array on the server. 
	   It is made in one request because of ‘All fields are required’ requirement. 
	   It could be implemented in extra request with a flag (for example ‘IsCompleted’) indicating if the website passes all validation. 
	   No compression, resizing or other image modifications are made. 
	7. It is possible to get all categories but not modifying or deleting them. 

## Any feedback you may wish to give about improving the assignment 

	The assignment is pretty interesting and challenging. 
