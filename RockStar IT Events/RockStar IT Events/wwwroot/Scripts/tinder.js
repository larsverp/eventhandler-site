function dislike() {
    document.getElementById('ratedimg').classList.add('dislike');
    setTimeout(newImg, 500);
    setTimeout(removedislike, 600);
}

function removedislike() {
    document.getElementById('ratedimg').classList.remove('dislike');
}

function like() {
    document.getElementById('ratedimg').classList.add('like');
    setTimeout(newImg, 500);
    setTimeout(removelike, 600);
}

function removelike() {
    document.getElementById('ratedimg').classList.remove('like');
}

function newImg() {
    console.log(count)

    if (categories.length <= count) {
        window.location.href = "/tinder/succes";
    }

    var url = categories[count].thumbnail;
    count++;
    document.getElementById("tinderImg").src = url;
}