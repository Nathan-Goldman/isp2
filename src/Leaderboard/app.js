var createError = require("http-errors");
var express = require("express");
var path = require("path");
var logger = require("morgan");
var fs = require("fs");
var favicon = require("serve-favicon");

var app = express();
app.listen(3001);

app.use(logger("dev"));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(favicon(path.join(__dirname, "/favicon.png")));

var scoresJson = fs.readFileSync("./scores.json", "utf8");
var scoresDict = JSON.parse(scoresJson) ?? {};

app.post("/submitscore", function(req, res, _) {
    let body = req.body;
    let keys = Object.keys(req.body);

    if (!keys.includes("uname") || !keys.includes("time")) {
        res.status(500);
        res.send("Improperly formatted json body.");
        return;
    }

    let time = Number(body.time);

    if (!(body.uname in scoresDict))
        scoresDict[body.uname] = time;
    else if (scoresDict[body.uname] > time)
        scoresDict[body.uname] = time;
    else {
        res.send("Submitted score, but it was lower than previous time of " + scoresDict[body.uname]);
        return;
    }

    fs.writeFileSync("./scores.json", JSON.stringify(scoresDict));

    console.log(scoresDict);

    res.send("Successfully submitted score");
});

app.get("/", function(_, res, _) {
    res.sendFile(path.join(__dirname, "/public/index.html"));
});

app.get("/scoresjson", function(_, res, _) {
    res.send(JSON.stringify(scoresDict));
});

// catch 404 and forward to error handler
app.use(function(_, _, next) {
    next(createError(404));
});

// error handler
app.use(function(err, req, res, _) {
    // set locals, only providing error in development
    res.locals.message = err.message;
    res.locals.error = req.app.get("env") === "development" ? err : {};

    // render the error page
    res.status(err.status || 500);
    res.send("error");
});

module.exports = app;
