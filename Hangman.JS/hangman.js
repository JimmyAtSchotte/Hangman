window.addEventListener("load", (event) => {
    createGame();
});

const baseUrl = 'https://localhost:7019';
const keepPlaying = 0;
const victory = 1;
const gameOver = 2;

function addGuessListner() {
    document.addEventListener('keyup', guess, false);
}

function removeGuessListner() {
    document.removeEventListener("keyup", guess);
}

function createGame()
{
    const httpRequest = new XMLHttpRequest();
    httpRequest.responseType = 'json';
    httpRequest.onload  = function() {
        render(httpRequest.response);
        addGuessListner();
    };
    httpRequest.open("POST", baseUrl +'/create-game', true);
    httpRequest.send();
}

function guess(event)
{
    const httpRequest = new XMLHttpRequest();
    httpRequest.responseType = 'json';
    httpRequest.onload  = function(response) {
        if(response.target.status === 200)
            render(httpRequest.response);
    };
    httpRequest.open("POST", baseUrl +'/guess', true);
    httpRequest.setRequestHeader("Content-Type", "application/json");
    httpRequest.send(JSON.stringify({
        "character": event.key,
        "gameId": document.getElementById("gameId").value
    }));
}

function playAgain(game) {
    switch (game.status) {
        case victory:
            return confirm("You won! Congratulations! Play again?")      
        case gameOver:
            return confirm("Game over. Correct word was " + game.wordProgress.join('') + ". Play again?")
    }
    
    return false;
}

function render(game)
{
    setGameId(game);
    renderWordProgress(game);
    renderRemainingGuesses(game);
    renderGuesses(game);
    
    if(game.status === keepPlaying)
        return;
    
    removeGuessListner();
        
    if(playAgain(game))
        createGame();   
}

function setGameId(game) {
    const gameId = document.getElementById("gameId");
    gameId.value = game.gameId;
}

function renderWordProgress(game) {
    const word = document.getElementById("word");
    word.innerHTML = "";

    for (let i = 0; i < game.wordProgress.length; i++) {
        const letter = document.createElement("li");
        letter.appendChild(document.createTextNode(game.wordProgress[i] ?? '_'));
        word.appendChild(letter);
    }
}

function renderGuesses(game) {
    const guesses = document.getElementById("guesses");
    guesses.innerHTML = "";
    
    if(game.guesses.length)
        document.getElementById("your-guesses").style.display = "block";
    else
        document.getElementById("your-guesses").style.display = "none";

    for (let i = 0; i < game.guesses.length; i++) {
        const guess = document.createElement("li");

        if (game.guesses[i].wordContainsCharacter)
            guess.classList.add("correct");
        else
            guess.classList.add("wrong");           
        
        guess.appendChild(document.createTextNode(game.guesses[i].character));
        guesses.appendChild(guess);
    }
}

function renderRemainingGuesses(game) {
    const remainingGuesses = document.getElementById("remaining-guesses");
    remainingGuesses.innerHTML = game.remainingGuesses;
}
