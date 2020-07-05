import {MDCTopAppBar} from '@material/top-app-bar';
import {MDCList} from "@material/list";
import {MDCDrawer} from "@material/drawer";

// Instantiation
const topAppBarElement = document.querySelector('.mdc-top-app-bar');
const topAppBar = new MDCTopAppBar(topAppBarElement);

const list = MDCList.attachTo(document.querySelector('.mdc-list'));
list.wrapFocus = true;

const drawer = MDCDrawer.attachTo(document.querySelector('.mdc-drawer'));

topAppBar.setScrollTarget(document.getElementById('body-content'));
topAppBar.listen('MDCTopAppBar:nav', () => {
  drawer.open = !drawer.open;
});

console.log("index.js loaded");