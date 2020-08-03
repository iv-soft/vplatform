import React from 'react';
import { Component } from "react";
var odatajs = require('odatajs');

@foreach(var item in (System.Collections.Generic.Dictionary<string, string>)@Parameters["Imports"])
{
@:import { @item.Key } from "@item.Value";
}

class @Parameters["Name"] extends Component {
  constructor(props) {
    super(props);
    this.state = {
      error: null,
      pending: true,
      data: null
    };
  }

  componentDidMount() {
      window.odatajs.oData.read(@Parameters["Source"],
            function(data) {
          	this.setState({
	            pending: false,
        	    data
	        });
            },
            function(error){
          	this.setState({
	            pending: false,
        	    error
	        });
            }
        );
    }
    render() { 
	const { error, pending, data } = this.state;
	if (error) {
      		return <div>Error: {error.message}</div>;
	} else if (pending) {
		return <div>Loading...</div>;
	} else {
	        return (
		      @Content
		);
	}
    }
}
 
export default @Parameters["Name"];