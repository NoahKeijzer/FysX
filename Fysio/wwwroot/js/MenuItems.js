function activeMenu1(){
    var menu1 = document.getElementById("menuButton1");
    menu1.classList.add("link-styled-active");

    var menu2 = document.getElementById("menuButton2");
    menu2.classList.remove("link-styled-active");

    var menu3 = document.getElementById("menuButton3");
    menu3.classList.remove("link-styled-active");
}

function activeMenu2() {
    var menu1 = document.getElementById("menuButton1");
    menu1.classList.remove("link-styled-active");

    var menu2 = document.getElementById("menuButton2");
    menu2.classList.add("link-styled-active");

    var menu3 = document.getElementById("menuButton3");
    menu3.classList.remove("link-styled-active");
}

function activeMenu3() {
    var menu1 = document.getElementById("menuButton1");
    menu1.classList.remove("link-styled-active");

    var menu2 = document.getElementById("menuButton2");
    menu2.classList.remove("link-styled-active");

    var menu3 = document.getElementById("menuButton3");
    menu3.classList.add("link-styled-active");
}

function disableMenu() {
    document.getElementById("menuButton1").style.pointerEvents = "none";
    document.getElementById("menuButton2").style.cursor = "default";
}