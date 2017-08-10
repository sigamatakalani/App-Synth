console.log('loading components');
import FlashGameComponent from './components/flash-game/flash-game.component';
import BlockGameComponent from './components/block-game/block-game.component';
import ShapesPatternGameComponent from './components/shapes-pattern/shapes-pattern-game.component';
import AuditoryArrowsGameComponent from './components/auditory-arrows/auditory-arrows-game.component';
import AuditoryGridGameComponent from './components/auditory-grid/auditory-grid-game.component';
import CountDownTimerComponent from './components/count-down-timer/count-down-timer.component';
import GridPatternGameComponent from './components/grid-pattern/grid-pattern-game.component';
import SpellingGameComponent from './components/spelling-game/spelling-game.component';
import ReadingGameComponent from './components/reading-game/reading-game.component';
import SequenceGameComponent from './components/sequence-game/sequence-game.component';
import LogicGameComponent from './components/logic-game/logic-game.component';
import LoadingIconComponent from './components/loading-icon/loading-icon.component';
import SettingsComponent from './components/settings/settings.component'
import SpecialCharComponent from './components/special-char/special-char.component'


const components = [
  {
    name: 'flashGame',
    fn: FlashGameComponent
  },
  {
    name: 'blockGame',
    fn: BlockGameComponent
  },
  {
    name: 'auditoryArrowsGame',
    fn: AuditoryArrowsGameComponent
  },
  {
    name: 'auditoryGridGame',
    fn: AuditoryGridGameComponent
  },
  {
    name: 'shapesPatternGame',
    fn: ShapesPatternGameComponent
  },
  {
    name: 'gridPatternGame',
    fn: GridPatternGameComponent
  },
  {
    name: 'spellingGame',
    fn: SpellingGameComponent
  },
  {
    name: 'readingGame',
    fn: ReadingGameComponent
  },
  {
    name: 'sequenceGame',
    fn: SequenceGameComponent
  },
  {
    name: 'logicGame',
    fn: LogicGameComponent
  },
  {
    name: 'countDownTimer',
    fn: CountDownTimerComponent
  },
  {
    name: 'loadingIcon',
    fn: LoadingIconComponent
  },
  {
    name: 'settings',
    fn: SettingsComponent
  },
  {
    name: 'specialChar',
    fn: SpecialCharComponent
  }
];

export default function(module){
  components.forEach(function(cmpnt) {
    //console.log('loading ' + cmpnt.name);
    module.component(cmpnt.name, cmpnt.fn);
  });
}
