//loops through an array in segments
var threadedLoop = function (array) {
    var self = this;

    //holds the threaded work
    var thread = {
        work: null,
        wait: null,
        index: 0,
        total: array.length,
        finished: false
    };

    //set the properties for the class
    this.collection = array;
    this.finish = function () { };
    this.action = function () { throw "You must provide the action to do for each element"; };
    this.interval = 1;

    //set this to public so it can be changed
    var chunk = parseInt(thread.total * .005);
    this.chunk = (chunk == NaN || chunk == 0) ? thread.total : chunk;

    //end the thread interval
    thread.clear = function () {
        window.clearInterval(thread.work);
        window.clearTimeout(thread.wait);
        thread.work = null;
        thread.wait = null;
    };

    //checks to run the finish method
    thread.end = function () {
        if (thread.finished) { return; }
        self.finish();
        thread.finished = true;
    };

    //set the function that handles the work
    thread.process = function () {
        if (thread.index >= thread.total) { return false; }

        //thread, do a chunk of the work
        if (thread.work) {
            var part = Math.min((thread.index + self.chunk), thread.total);
            while (thread.index < part) {
                self.action(self.collection[thread.index], thread.index, thread.total);
                thread.index++;
            }
        }
        else {

            //no thread, just finish the work
            while (thread.index++ < thread.total) {
                self.action(self.collection[thread.index], thread.index, thread.total);
            }
        }

        //check for the end of the thread
        if (thread.index >= thread.total) {
            thread.clear();
            thread.end();
        }

        //return the process took place
        return true;

    };

    //set the working process
    self.start = function () {
        thread.finished = false;
        thread.index = 0;
        thread.work = window.setInterval(thread.process, self.interval);

        return thread.work;
    };

    //stop threading and finish the work
    self.wait = function (timeout) {

        //create the waiting function
        var complete = function () {
            thread.clear();
            thread.process();
            thread.end();
        };

        //if there is no time, just run it now
        if (!timeout) {
            complete();
        }
        else {
            thread.wait = window.setTimeout(complete, timeout);
        }
    };

};

// Note: this class is not battle-tested, just personal testing on large arrays