import React from 'react';
import { Component } from "react";
@foreach(var item in (System.Collections.Generic.Dictionary<string, string>)@Parameters["Imports"])
{
@:import { @item.Key } from "@item.Value";
}

class @Parameters["Name"] extends Component {
    state = {  }
    render() { 
        return (
	      @Content
	);
    }
}
 
export default @Parameters["Name"];