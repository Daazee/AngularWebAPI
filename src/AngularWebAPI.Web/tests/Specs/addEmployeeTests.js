describe('Add Employee Tests', function () {

    var path = require('path');
    var fileToUpload = '../profile-icon-9.png';
    var absolutePath = path.resolve(__dirname, fileToUpload);
    

    beforeEach(() => {
        browser.manage().window().setSize(1600, 1000);
        browser.get('http://employeesystem.azurewebsites.net/#/EmployeeList');
    });

    it('should add a new employee with dependant', function() {        

        var initialRows = element.all(by.repeater('employee in model.employees')).count();

        element(by.css(".glyphicon-pencil")).click();

        let firstname = "Helen";
        let lastname = "Miller";
        let dob = "01-30-1991";
        let gender = "Female";
        let position = "Developer";
        let dependantFirstname = "Richard";
        let dependantLastname = "Miller";
        let dependantGender = "Male";
        let dependantRelationship = "Child";

        //Add Employee
        element(by.model("model.firstname")).sendKeys(firstname);
        element(by.model("model.lastname")).sendKeys(lastname);
        element(by.model("model.dateOfBirth")).sendKeys(dob);
        element(by.model("model.gender")).sendKeys(gender);
        element(by.model("model.position")).sendKeys(position);

        let addEmployeeButton = element.all(by.css("input[type=submit]")).get(0);

        addEmployeeButton.click();

        //expect(browser.driver.findElement(by.id('imageFile')).isDisplayed()).toBe(true);

        //Upload Picture
        element(by.id('imageFile')).sendKeys(absolutePath);
        element.all(by.css("input[type=submit]")).get(1).click();

        //Add Dependant

        element(by.model("model.dependantFirstname")).sendKeys(dependantFirstname);
        element(by.model("model.dependantLastname")).sendKeys(dependantLastname);
        element(by.model("model.dependantGender")).sendKeys(dependantGender);
        element(by.model("model.dependantRelationship")).sendKeys(dependantRelationship);

        element.all(by.css("input[type=submit]")).get(2).click();
        
        element(by.css(".btn-success")).click();
        var finalRows = element.all(by.repeater('employee in model.employees')).count();

        expect(initialRows).not.toEqual(finalRows);        
    })

    it('should add a new employee without dependant', function () {        

        var initialRows = element.all(by.repeater('employee in model.employees')).count();

        element(by.css(".glyphicon-pencil")).click();

        let firstname = "Zacharias";
        let lastname = "Holmes";
        let dob = "02-10-1991";
        let gender = "Male";
        let position = "Manager";

        //Add Employee
        let firstnameField = element(by.model("model.firstname"));
        firstnameField.sendKeys(firstname);

        let lastnameField = element(by.model("model.lastname"));
        lastnameField.sendKeys(lastname);

        let dobField = element(by.model("model.dateOfBirth"));
        dobField.sendKeys(dob);

        element(by.model("model.gender")).sendKeys(gender);
        element(by.model("model.position")).sendKeys(position);

        let addEmployeeButton = element.all(by.css("input[type=submit]")).get(0);

        addEmployeeButton.click();

        //Upload Picture
        element(by.id('imageFile')).sendKeys(absolutePath);
        element.all(by.css("input[type=submit]")).get(1).click();

        element(by.css(".btn-success")).click();
        
        var finalRows = element.all(by.repeater('employee in model.employees')).count();

        expect(initialRows).not.toEqual(finalRows);

    })

});