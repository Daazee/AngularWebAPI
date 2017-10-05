describe('Employee List Tests', function () {
    
    beforeEach(() => {
        browser.manage().window().setSize(1600, 1000);
        browser.get('http://employeesystem.azurewebsites.net/#/EmployeeList');
    });

    it('should search for employee', function () {

        // Find the element with ng-model="user" and type "jacksparrow" into it
        element(by.model('searchText')).sendKeys('John');

        // Verify that a record exists
        expect(element.all(by.repeater('employee in model.employees')).count()).not.toEqual(0);
    })

    it('should view details of an employee', function () {

        let expectedID = '39';

        element(by.model('searchText')).sendKeys('John');

        // Verify that a record exists
        expect(element.all(by.repeater('employee in model.employees')).count()).not.toEqual(0);

        // Find the Detail button and click it
        element(by.css('.btn-success')).click();

        //Get the Employee ID from the second column of the first row 
        let idText = element(by.css('tr:first-child td:nth-child(2)')).getText();
         
        //Verify expected Employee ID
        expect(idText).toEqual(expectedID);
    })

    it('should edit an employee', function () {
       
        let employee = element.all(by.css(".btn-primary")).first();
        employee.click();

        let firstname = "Dan";
        let lastname = "Test";
        let gender = "Male";
        let position = "Manager";

        let firstnameField = element(by.model("model.employee.firstname"));
        firstnameField.clear();
        firstnameField.sendKeys(firstname);

        let lastnameField = element(by.model("model.employee.lastname"));
        lastnameField.clear();
        lastnameField.sendKeys(lastname);

        let genderOption = element(by.model("model.employee.gender"));
        genderOption.sendKeys(gender);

        let positionOption = element(by.model("model.employee.position"));
        positionOption.sendKeys(position);

        let updateButton = element(by.css("input[type=submit]"));
        updateButton.click();
                
        let expectedFirstname = element.all(by.repeater("employee in model.employees").row(0).column("employee.firstname"));
        let expectedLastname = element.all(by.repeater("employee in model.employees").row(0).column("employee.lastname"));
        let expectedGender = element.all(by.repeater("employee in model.employees").row(0).column("employee.gender"));
        let expectedPosition = element.all(by.repeater("employee in model.employees").row(0).column("employee.position"));

        expect(expectedFirstname.getText()).toEqual([firstname]);
        expect(expectedLastname.getText()).toEqual([lastname]);
        expect(expectedGender.getText()).toEqual([gender]);
        expect(expectedPosition.getText()).toEqual([position]);
    })

    it('should delete an employee', function () {        

        var initialRows = element.all(by.repeater('employee in model.employees')).count();

        //Find the Delete button and click it
        let firstEmployee = element.all(by.css('.btn-danger')).get(3);
        firstEmployee.click();

        browser.switchTo().alert().accept();        

        var finalRows = element.all(by.repeater('employee in model.employees')).count();

        expect(initialRows).not.toEqual(finalRows);
    })

});
