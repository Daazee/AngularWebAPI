import { AppPage } from './app.po';

import { EmployeeListPage } from './employee/employee-list.po';

describe('Conduit App E2E Test Suite', () => {
    let page: AppPage;
    let employeeListPage: EmployeeListPage;

    beforeEach(() => {
        employeeListPage = new EmployeeListPage();
  });

    //it('should display welcome message', () => {
    //    employeeListPage.navigateTo();
    //    expect(employeeListPage.getTitle()).toEqual('Employee List');
    //});


});
