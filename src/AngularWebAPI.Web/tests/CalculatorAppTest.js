describe('Calculator App Tests', function () {
    beforeEach(module('MyApp'));
    describe('reversestringfiltertest', function () {
        var reverse;
        beforeEach(inject(function ($filter) { //initialize filter
            reverse = $filter('reverse', {});
        }));
        it('Should reverse a string', function () {
            expect(reverse('india')).toBe('aidni'); //pass test
            expect(reverse('don')).toBe('nod'); //fail test
        });
    });
});