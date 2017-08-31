//describe('AngularJS 2-way binding test', () => {

//    it('Should bind the name you enter into the input box', () => {

//        browser.get('https://angularjs.org');

//        element(by.model('yourName')).sendKeys('World');

//        const response = element(by.css('h1.ng-binding'));

//        expect(response.getText()).toEqual('Hello World!');

//    });

//});

describe('Employee search test', function () {
    it('should filter results', function () {

        browser.get('http://employeesystem.azurewebsites.net/#/EmployeeList');

        // Find the element with ng-model="user" and type "jacksparrow" into it
        element(by.model('searchText')).sendKeys('John');

        // Find the first (and only) button on the page and click it
        //element(by.css(':button')).click();

        // Verify that there are 10 tasks
        expect(element.all(by.repeater('employee in model.employees')).count()).toEqual(1);

        // Enter 'groceries' into the element with ng-model="filterText"
        //element(by.model('filterText')).sendKeys('groceries');

        //// Verify that now there is only one item in the task list
        //expect(element.all(by.repeater('task in tasks')).count()).toEqual(1);
    });
});



