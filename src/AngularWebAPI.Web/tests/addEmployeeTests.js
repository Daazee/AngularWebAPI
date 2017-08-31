describe('Add Employee Tests', function() {

    it('should add a new employee', function() {

        browser.get('http://employeesystem.azurewebsites.net/#/AddEmployee');

        let firstname = "Bojan";
        let lastname = "Helen";
        let dob = "01-30-1991";
        let gender = "Male";
        let position = "Developer";

        let firstnameField = element(by.model("model.firstname"));
        firstnameField.sendKeys(firstname);

        let lastnameField = element(by.model("model.lastname"));
        lastnameField.sendKeys(lastname);

        let dobField = element(by.model("model.dateOfBirth"));
        dobField.sendKeys(dob);

        element(by.model("model.gender")).sendKeys(gender);
        element(by.model("model.position")).sendKeys(position);

        element.all(by.css("input[type=submit]")).get(0).click();

        

    })

});