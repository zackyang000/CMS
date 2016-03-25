import React from 'react';
import { shallow } from 'enzyme';
import { expect } from 'chai';
import Home from '../src/app/home/containers/Home';

describe('<Home />', () => {
  it('should render stuff', () => {
    const wrapper = shallow(<Home />);
    expect(wrapper.length).to.equal(1);
  });
});

