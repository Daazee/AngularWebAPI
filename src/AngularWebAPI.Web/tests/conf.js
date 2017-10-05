
exports.config = {    

    capabilities: {
        'browserName': 'chrome'
    },

    seleniumAddress: 'http://localhost:4444/wd/hub',

    //specs: ['Specs/addEmployeeTests.js', 'Specs/employeeListTests.js']
    specs: ['Specs/employeeListTests.js']
};

//exports.config = {
//    'seleniumAddress': 'http://hub-cloud.browserstack.com/wd/hub',

//    specs: ['Specs/employeeListTests.js'],

//    'baseUrl': 'http://employeesystem.azurewebsites.net/#/EmployeeList',

//    'capabilities': {
//        'browserstack.user': 'jesupelumiadetun1',
//        'browserstack.key': 'QBSxmy5VkEEzcena6943',
//        'os': 'Windows',
//        'os_version': '10',
//        'browserName': 'Chrome',
//        'browser_version': '60.0',
//        'resolution': '1024x768'
//    }
//};

