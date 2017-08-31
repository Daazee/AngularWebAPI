
exports.config = {

    capabilities: {
        'browserName': 'chrome'
    },

    seleniumAddress: 'http://localhost:4444/wd/hub',

    specs: ['addEmployeeTests.js']
};

//exports.config = {
//    'seleniumAddress': 'http://hub-cloud.browserstack.com/wd/hub',

//    specs: ['test-spec.js'],

//    'baseUrl': 'http://employeesystem.azurewebsites.net/#/EmployeeList',

//    'capabilities': {
//        'browserstack.user': 'jesupelumiadetun1',
//        'browserstack.key': 'QBSxmy5VkEEzcena6943',
//        'os': 'Windows',
//        'os_version': '7',
//        'browserName': 'Chrome',
//        'browser_version': '52.0',
//        'resolution': '1024x768'
//    }
//};