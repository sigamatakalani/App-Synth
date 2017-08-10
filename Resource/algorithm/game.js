import {InvalidLevelException, InvalidStageException, UnimplementedException} from './exceptions';

export class Game {
  /**
   * @class Game
   * @classdesc Base class for Games. This contains all the base behaviour
   * expected of a game.
   *
   * @prop {Object} level           - The level object used to generate stages.
   * @prop {int} points             - Points earned during game session.
   * @prop {int} timePlayed         - Time the user has played for in milliseconds.
   * @prop {int} hintCount          - The number of times the user has requested
   *                                  a hint.
   * @prop {boolean} paused         - Boolean representing whether the game is
   *                                  paused or not.
   * @prop {Stage} stage            - The current stage the user is playing.
   * @prop {Array} levelProperties  - Properties expecting to be present in
   *                                  a level object, this is used to test the
   *                                  validity of a given level.
   * @prop {Any} answer             - Player's answer for current stage.
   * @prop {Function} timeout       - Timeout function to be used, since this
   *                                  is abstracted from any ui and is meant to
   *                                  work solely as a logical backend, it has
   *                                  no access to $timeout variable provided by
   *                                  angularJs which performs $scope.apply to
   *                                  update changes to the $scope. If a timeout
   *                                  function is not specified, the default
   *                                  setTimeout will be used(one such case
   *                                  would be in the case of unit testing).
   *
   * @prop {bool}   timeLeftTimerPaused       - Flag used to toggle pause on @prop timeLeftTimer
   * @prop {float}  timeLeftPercentage        - Percentage of how much time is left
   * @prop {int}    timeLeftTimer             - Timer ID of the timer used to calculate how much time
   *                                            is left
   * @prop {int}    timeLeftTimerStartPoint   - Point in time where timer became active
   *
   * @constructor
   */
  constructor(timeout=null, interval=null){
    this.level = null;
    this.points = 0;
    this.timePlayed = 0;
    this.interval = interval;
    this.timeout = timeout;
    this.hintCount = 0;
    this.paused = false;
    this.stage = null;
    this.timer = null;
    this.answer = null;
    this.started = false;
    this.levelProperties = [];
    this.draggablesDisabled = true;
    this.draggablesHidden = false;
    this.onReady = null;
    this.COLOURS = [
      'blue', 'green', 'red', 'yellow', 'black', 'white'
    ];
    //add these to comments
    this.timeLeftTimerPaused = false;
    this.timeLeftPercentage= null;
    this.timeLeftTimer= null;
    this.timeLeftTimerStartPoint = 0;
    this.utimeCallback = null;
    this.utimeLeft = null;
    this.startTime = 0;
  }

  setUniversalTimeLeft(timeLeft, callback){
    this.utimeLeft= timeLeft;
    this.utimeCallback = callback;
  }

  disableDraggables(){
    this.draggablesDisabled = true;
  }

  enableDraggables(){
    this.draggablesDisabled = false;
  }

  draggablesAreDisabled(){
    return this.draggablesDisabled;
  }

  hideDraggables(){
    this.draggablesHidden = true;
  }

  showDraggables(){
    this.draggablesHidden = false;
  }

  draggablesAreHidden(){
    return this.draggablesHidden;
  }

  /**
   * @desc  Resets time left timer on a game, this to be used with
   *        games that give player a limited time to complete a stage
   *
   */
  resetTimeLeftTimer(blockAmount=null){
    if(this.timeLeftTimer !== null)
      this.clearInterval(this.timeLeftTimer);
    this.timeLeftTimerPaused = false;
    this.timeLeftTimerStartPoint = this.timePlayed;
    let complete = (this.level.timeToComplete * 2) + 1000; //time hack

    ///-- NEO's things
    if(blockAmount){
      complete = this.level.timeToComplete;

      complete = complete * blockAmount;

      if(complete < 10000)
      {
        complete = 10000;
      }
    }
    ///---
    this.timeLeftTimer = this.setInterval(()=>{
      if(this.timeLeftTimerPaused){
        //increment so the gap  between the startpoint and timeplayed
        //remains the same
        this.timeLeftTimerStartPoint ++;
        return;
      }
      let ticked = this.timePlayed - this.timeLeftTimerStartPoint;
      this.timeLeftPercentage = ((complete - ticked) / complete) * 100;
      if(this.timeLeftPercentage <= 0){
        this.timeLeftPercentage = 0;
        this.clearInterval(this.timeLeftTimer);
        this.stage.failed = true;

      }
    }, 1);
  }

  /**
   * @desc  Dispatch the appropriate timeout method based on @prop timeout.
   *        This is merely a wrapper for whichever timeout method was set
   *        by @prop timeout
   *
   */
  setTimeout(){
    if(this.timeout !=null){
      return this.timeout.apply(null, [].slice.call(arguments, 0));
    }else{
      return setTimeout.apply(null, [].slice.call(arguments, 0));
    }
  }

  setInterval(){
    if(this.interval !=null){
      return this.interval.apply(null, [].slice.call(arguments, 0));
    }else{
      return setInterval.apply(null, [].slice.call(arguments, 0));
    }
  }

  getuTimeLeft(){
    this.utimeLeft = Date.now() - this.startTime;
  }
  /**
   * @desc  Get the number of points earned
   * @return {int}
   */
  getPoints(){
    return this.points;
  }

  /**
   * @desc  Validates the current level.
   * @throws {InvalidLevelException}
   *
   */
  validateLevel(level){
    if(level === null)
      throw new InvalidLevelException();

    for(let prop of this.levelProperties){
      if(!level.hasOwnProperty(prop))
        throw new InvalidLevelException();
    }
  }

  /**
   * @desc  Return the time spent playing a stage in milliseconds.
   * @return {int}
   */
  getStageTimePlayed(){
    if(this.stage === null)
      return 0;
    return this.getTimePlayed() - this.stage.getTimeStarted();
  }


  /**
  *@desc
  *
  */

  getStageAttemptData(){
    if(this.stage.attemptData){
      return this.stage.attemptData;
    }else {
      return null;
    }
  }

  /**
   * @desc  Start the timer for the game
   *
   */
  startTimer(){
    //tick every millisecond
    this.startTime = Date.now();
    console.log(this.startTime);
    console.log(this.utimeLeft);
    this.timer = this.setInterval(()=>{
      //only tick if game isn't paused and draggables are enabled
      if(!this.draggablesDisabled){
        this.tick();
      }
      if(this.utimeCallback){
        this.utimeLeft -= 4;
        if(this.utimeLeft <= 0){
          this.utimeLeft = 0;
          this.utimeCallback();
        }
      }
    }, 1);
  }

  /**
   * @desc Increase the time played by 1
   *
   */
  tick(){
    //this.timePlayed ++;
    this.timePlayed += 4;
  }

  /**
   * @desc Returns a boolean stating whether or not the game is paused
   * return {boolean}
   */
  isPaused(){
    return this.paused;
  }

  /**
   * @desc Resets the timer on the game
   *
   */
  resetTimer(){
    this.timePlayed = 0;
  }

  /**
   * @desc  Reset everything before starting a new stage
   *        Add earned points to @prop points.
   *        Set stage to null
   *
   */
  clearStage(){
    if(this.isStageCompleted())
      this.points += this.getStage().getPoints();
    delete this.getStage();
    this.stage = null;
  }

  /**
   * @desc Get current game stage
   * return {Stage}
   */
  getStage(){
    return this.stage;
  }

  /**
   * @desc Get current game level
   * return {Object}
   */
  getLevel(){
    return this.level;
  }

  /**
   * @desc Set current game level
   *
   */
  setLevel(level){
    this.validateLevel(level);
    this.level = level;
  }

  /**
   * @desc Returns true if the current stage is completed
   * @return {boolean}
   */
  isStageCompleted(){
    if(this.getStage() === null)
      return false;
    return this.getStage().isCompleted();
  }

  /**
   * @desc Returns true if the current stage has been failed
   * @return {boolean}
   */
  isStageFailed(){
    if(this.getStage() === null)
      return false;
    return this.getStage().failed;
  }

  /**
   * @desc Returns true if the current stage has been started
   * @return {boolean}
   */
  hasStageStarted(){
    if(this.getStage() === null)
      return false;
    return this.getStage().getTimeStarted() !== null;
  }

  /**
   * @desc Return time played
   * return {int}
   */
  getTimePlayed(){
    return this.timePlayed;
  }

  /**
   * @desc  Create a stage based on the current level, levels determine properties of
   *        a stage that influence it's difficulty stages are randomized games
   *        created that match the confines of a level.
   *        Sets @prop stage to the newly generatedStage.
   *        Throws exception if the level is not valid.
   *
   */
  generateStage(){
    throw new UnimplementedException();
  }

  /**
   * @desc  Clear game screen and prepare all necessary objects for stage
   * @param {Stage} stage - The stage you want the game to prepare to play
   * @throws {InvalidStageException}
   *
   */
  prepareStage(){
    if(this.getStage() === null)
      throw new InvalidStageException();
  }

  /**
   * @desc  Show a hint to the stage's solution
   *
   */
  hint(){
    this.hintCount++;
  }

  /**
   * @desc  Return the number of times player asked for hint
   *
   * @return {int}
   */
  getHintCount(){
    return this.hintCount;
  }


  /**
   * @desc  Pause the game by hiding it's contents and stop incrementing the timePlayed counter
   *
   */
  pause(){
    this.paused = true;
  }

  /**
   * @desc  Resume the game, revealing contents and starting the counter again
   *
   */
  resume(){
    this.paused = false;
  }

  /**
   * @desc  Set game's current answer to the given value, if the stage
   *        is not yet completed
   * @param {Any} answer  - Answer to set
   */
  setAnswer(answer){
    if(!this.stage.completed)
      this.answer = answer;
  }

  /**
   * @desc  Checks to see if an answer is the correct answer for the stage
   *
   */
  checkAnswer(){
    if(this.getStage() == null)
      throw new InvalidStageException();
    return this.getStage().checkAnswer(this.answer);
  }

  /**
   * @desc  Display solution for current stage
   *
   */
  showSolution(){
      throw new UnimplementedException();
  }

  /**
   * @desc  Play a given level.
   * @param {int} level  - The level you want to start playing at
   * return {undefined}
   */
  start(level){
    this.hintCount =0;
    console.log("Setting Level");
    this.setLevel(level);
    console.log("generating stage");
    this.generateStage();
    console.log("preparing stage");
    this.prepareStage();
    console.log("getting stage");
    this.getStage().start(this);
    if(this.timer === null)
      this.startTimer();
    this.started = true;
    if(this.onReady)
      this.onReady();
  }

  clearInterval(interval){
    if(this.interval){
      this.interval.cancel(interval);//angularjs
    }else{
      clearInterval(interval);
    }
  }

  /**
   * @desc  Stops game, and returns score and other relevant information
   * @return {Object}
   */
  finish(){
    //stop interval
    this.clearInterval(this.timer);
    this.timer = null;
    this.clearStage();
    this.started = false;
    return {
      timePlayed: this.getTimePlayed(),
      pointsEarned: this.getPoints(),
      hints: this.getHintCount(),
    };
  }
}
