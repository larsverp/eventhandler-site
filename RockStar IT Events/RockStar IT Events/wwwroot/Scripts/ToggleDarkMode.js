var toggleButton = document.getElementById("ToggleButton");
var root = document.documentElement;
var toggle = false;

toggleButton.addEventListener("click", (e) => {
    toggle = !toggle;
    console.log(toggle);

    if (toggle == false) {
        root.style.setProperty('--backgroundColor', '#333333');
        root.style.setProperty('--eventBLockColor', '#232323');
        root.style.setProperty('--textColorDark', '#000000');
        root.style.setProperty('--textColorLight', '#ffffff');
        root.style.setProperty('--yellowColor', '#FFE000');
        root.style.setProperty('--invertAmount', '100%');

    }
    if (toggle == true) {
        root.style.setProperty('--eventBLockColor', '#ffffff');
        root.style.setProperty('--textColorDark', '#ffffff');
        root.style.setProperty('--backgroundColor', '#ffffff');
        root.style.setProperty('--textColorLight', '#000000');
        root.style.setProperty('--yellowColor', '#FFE000');
        root.style.setProperty('--invertAmount', '0');
    }
})