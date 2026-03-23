import React, { useState, useEffect } from 'react';
import { Settings, Code, Coffee, Zap, Heart, Brain, Wrench } from 'lucide-react';

const JavaRobotPet = () => {
  const [robotState, setRobotState] = useState({
    name: 'JBot',
    level: 1,
    experience: 0,
    energy: 100,
    happiness: 80,
    specialization: 'Généraliste',
    mood: 'Curieux',
    skills: {
      backend: 10,
      gui: 5,
      mobile: 3,
      debugging: 8
    },
    stats: {
      linesOfCode: 0,
      bugsSolved: 0,
      coffeeBreaks: 0,
      lastCoding: null
    }
  });

  const [codingSession, setCodingSession] = useState({
    isActive: false,
    startTime: null,
    currentProject: '',
    language: 'Java'
  });

  const [logs, setLogs] = useState([
    "🤖 JBot s'initialise...",
    "☕ Prêt à coder en Java !"
  ]);

  // Simulation de l'évolution basée sur les habitudes
  const simulateCodingHabits = (type) => {
    setRobotState(prev => {
      const newState = { ...prev };
      
      // Augmentation d'expérience et de compétences
      newState.experience += 15;
      newState.stats.linesOfCode += Math.floor(Math.random() * 50) + 10;
      
      // Spécialisation selon le type de code
      switch(type) {
        case 'backend':
          newState.skills.backend += 3;
          if (newState.skills.backend > 50 && prev.specialization === 'Généraliste') {
            newState.specialization = 'Expert Backend';
            addLog("🎯 JBot se spécialise en Backend Java !");
          }
          break;
        case 'gui':
          newState.skills.gui += 3;
          if (newState.skills.gui > 40 && prev.specialization === 'Généraliste') {
            newState.specialization = 'Expert Interface';
            addLog("🎨 JBot devient expert en interfaces Java !");
          }
          break;
        case 'debug':
          newState.skills.debugging += 2;
          newState.stats.bugsSolved += 1;
          newState.happiness += 5;
          break;
      }
      
      // Level up
      if (newState.experience >= newState.level * 100) {
        newState.level += 1;
        newState.happiness += 10;
        addLog(`🆙 JBot atteint le niveau ${newState.level} !`);
      }
      
      // Changement d'humeur selon l'activité
      if (newState.happiness > 90) newState.mood = 'Euphorique';
      else if (newState.happiness > 70) newState.mood = 'Content';
      else if (newState.happiness > 50) newState.mood = 'Concentré';
      else newState.mood = 'Fatigué';
      
      newState.stats.lastCoding = new Date().toLocaleTimeString();
      
      return newState;
    });
  };

  const addLog = (message) => {
    setLogs(prev => [message, ...prev.slice(0, 4)]);
  };

  const startCodingSession = (projectType) => {
    setCodingSession({
      isActive: true,
      startTime: new Date(),
      currentProject: projectType,
      language: 'Java'
    });
    addLog(`💻 Session ${projectType} démarrée`);
  };

  const endCodingSession = () => {
    if (codingSession.isActive) {
      const duration = Math.floor((new Date() - codingSession.startTime) / 1000);
      simulateCodingHabits(codingSession.currentProject);
      addLog(`✅ Session terminée (${duration}s)`);
    }
    setCodingSession({ ...codingSession, isActive: false });
  };

  const takeBreak = () => {
    setRobotState(prev => ({
      ...prev,
      energy: Math.min(100, prev.energy + 15),
      stats: {
        ...prev.stats,
        coffeeBreaks: prev.stats.coffeeBreaks + 1
      }
    }));
    addLog("☕ Pause café avec JBot !");
  };

  // Animation de l'énergie qui diminue pendant le codage
  useEffect(() => {
    let interval;
    if (codingSession.isActive) {
      interval = setInterval(() => {
        setRobotState(prev => ({
          ...prev,
          energy: Math.max(0, prev.energy - 1)
        }));
      }, 2000);
    }
    return () => clearInterval(interval);
  }, [codingSession.isActive]);

  const getRobotExpression = () => {
    if (robotState.energy < 20) return '😴';
    if (robotState.happiness > 85) return '🤖✨';
    if (codingSession.isActive) return '🤖💻';
    if (robotState.mood === 'Content') return '🤖😊';
    return '🤖';
  };

  const getSpecializationColor = () => {
    switch(robotState.specialization) {
      case 'Expert Backend': return 'text-blue-600';
      case 'Expert Interface': return 'text-purple-600';
      case 'Expert Mobile': return 'text-green-600';
      default: return 'text-gray-600';
    }
  };

  return (
    <div className="max-w-4xl mx-auto p-6 bg-gradient-to-br from-slate-50 to-blue-50 min-h-screen">
      <div className="bg-white rounded-2xl shadow-xl p-8">
        {/* En-tête */}
        <div className="text-center mb-8">
          <div className="text-6xl mb-4">{getRobotExpression()}</div>
          <h1 className="text-3xl font-bold text-gray-800 mb-2">{robotState.name}</h1>
          <p className={`text-lg ${getSpecializationColor()} font-semibold`}>
            {robotState.specialization} Java • Niveau {robotState.level}
          </p>
          <p className="text-gray-600">Humeur: {robotState.mood}</p>
        </div>

        {/* Barres de statut */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
          <div className="bg-gradient-to-r from-red-100 to-red-200 p-4 rounded-lg">
            <div className="flex items-center gap-2 mb-2">
              <Zap className="text-red-600" size={20} />
              <span className="font-semibold">Énergie</span>
            </div>
            <div className="w-full bg-red-200 rounded-full h-3">
              <div 
                className="bg-red-500 h-3 rounded-full transition-all duration-300"
                style={{ width: `${robotState.energy}%` }}
              ></div>
            </div>
            <span className="text-sm text-gray-600">{robotState.energy}/100</span>
          </div>

          <div className="bg-gradient-to-r from-yellow-100 to-yellow-200 p-4 rounded-lg">
            <div className="flex items-center gap-2 mb-2">
              <Heart className="text-yellow-600" size={20} />
              <span className="font-semibold">Bonheur</span>
            </div>
            <div className="w-full bg-yellow-200 rounded-full h-3">
              <div 
                className="bg-yellow-500 h-3 rounded-full transition-all duration-300"
                style={{ width: `${robotState.happiness}%` }}
              ></div>
            </div>
            <span className="text-sm text-gray-600">{robotState.happiness}/100</span>
          </div>

          <div className="bg-gradient-to-r from-blue-100 to-blue-200 p-4 rounded-lg">
            <div className="flex items-center gap-2 mb-2">
              <Brain className="text-blue-600" size={20} />
              <span className="font-semibold">Expérience</span>
            </div>
            <div className="w-full bg-blue-200 rounded-full h-3">
              <div 
                className="bg-blue-500 h-3 rounded-full transition-all duration-300"
                style={{ width: `${(robotState.experience % 100)}%` }}
              ></div>
            </div>
            <span className="text-sm text-gray-600">{robotState.experience} XP</span>
          </div>
        </div>

        {/* Actions de codage */}
        <div className="mb-8">
          <h3 className="text-xl font-semibold mb-4 flex items-center gap-2">
            <Code size={24} />
            Sessions de Codage Java
          </h3>
          
          {!codingSession.isActive ? (
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              <button
                onClick={() => startCodingSession('backend')}
                className="bg-blue-500 hover:bg-blue-600 text-white p-4 rounded-lg transition-colors"
              >
                <Wrench size={24} className="mx-auto mb-2" />
                <div>Backend/API</div>
                <div className="text-sm opacity-80">Spring, servlets...</div>
              </button>
              
              <button
                onClick={() => startCodingSession('gui')}
                className="bg-purple-500 hover:bg-purple-600 text-white p-4 rounded-lg transition-colors"
              >
                <Settings size={24} className="mx-auto mb-2" />
                <div>Interface GUI</div>
                <div className="text-sm opacity-80">Swing, JavaFX...</div>
              </button>
              
              <button
                onClick={() => startCodingSession('debug')}
                className="bg-red-500 hover:bg-red-600 text-white p-4 rounded-lg transition-colors"
              >
                <Code size={24} className="mx-auto mb-2" />
                <div>Debug & Tests</div>
                <div className="text-sm opacity-80">JUnit, debugging...</div>
              </button>
            </div>
          ) : (
            <div className="bg-green-100 p-6 rounded-lg text-center">
              <div className="text-green-800 font-semibold mb-2">
                🟢 Session {codingSession.currentProject} en cours...
              </div>
              <div className="text-green-600 mb-4">
                JBot vous observe coder et apprend de vos habitudes !
              </div>
              <button
                onClick={endCodingSession}
                className="bg-green-500 hover:bg-green-600 text-white px-6 py-2 rounded-lg transition-colors"
              >
                Terminer la session
              </button>
            </div>
          )}
        </div>

        {/* Actions */}
        <div className="mb-8">
          <button
            onClick={takeBreak}
            className="bg-amber-500 hover:bg-amber-600 text-white px-6 py-3 rounded-lg transition-colors flex items-center gap-2 mx-auto"
          >
            <Coffee size={20} />
            Pause café avec JBot
          </button>
        </div>

        {/* Compétences */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-8">
          <div className="bg-gray-50 p-6 rounded-lg">
            <h3 className="font-semibold mb-4">Compétences Java</h3>
            <div className="space-y-3">
              {Object.entries(robotState.skills).map(([skill, level]) => (
                <div key={skill}>
                  <div className="flex justify-between mb-1">
                    <span className="capitalize">{skill}</span>
                    <span>{level}</span>
                  </div>
                  <div className="w-full bg-gray-200 rounded-full h-2">
                    <div 
                      className="bg-gradient-to-r from-blue-400 to-blue-600 h-2 rounded-full transition-all duration-500"
                      style={{ width: `${Math.min(100, level * 2)}%` }}
                    ></div>
                  </div>
                </div>
              ))}
            </div>
          </div>

          <div className="bg-gray-50 p-6 rounded-lg">
            <h3 className="font-semibold mb-4">Statistiques</h3>
            <div className="space-y-2">
              <div>📝 Lignes de code: {robotState.stats.linesOfCode}</div>
              <div>🐛 Bugs résolus: {robotState.stats.bugsSolved}</div>
              <div>☕ Pauses café: {robotState.stats.coffeeBreaks}</div>
              <div>⏰ Dernière session: {robotState.stats.lastCoding || 'Jamais'}</div>
            </div>
          </div>
        </div>

        {/* Journal d'activité */}
        <div className="bg-slate-800 text-green-400 p-4 rounded-lg font-mono text-sm">
          <div className="font-semibold mb-2">📊 Journal de JBot:</div>
          {logs.map((log, index) => (
            <div key={index} className="mb-1">
              &gt; {log}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default JavaRobotPet;