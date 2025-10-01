import { AppComponent } from './app.component';

describe('AppComponent', () => {
  it('should have a truthy class definition', () => {
    // Just test that the AppComponent class exists and is defined
    expect(AppComponent).toBeDefined();
    expect(typeof AppComponent).toBe('function');
  });
});