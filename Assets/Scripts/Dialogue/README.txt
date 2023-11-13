Шаг 1. Создаем обьект с колайдером(галочка isTrigger), добавляем на него DialogueAnimator.cs(Нужные поля заполнить после шага 5  )
 или добавляем в Children в иерархии на уже готовый обьект префабы DialogueManager.prefab и dialogue_canvas.prefab;
Нажимаем на Каждый префаб в иерархии Prefab-Unpack Completely.
Шаг2. в DialogueManager.prefab в DialogueManager.cs заполняем все поля в инспекторе
(нужные поля находятся в dialogue_canvas, аниматор GameObject DialogueBox в канвасе).
Шаг3.dialogue_canvas по умолчанию компонент канвас выключен, он включится когда игрок зайдет в колайдер.
в Continue Button переносим GameObject DialogueManager выбираем DisplayNExtSentenses.
Шаг4. в SayToNpc добвавляем скрипт DialogueTrigger.cs Указываем Имя персонажа, количество предложений. В DialogueManager перетаскиваем текущий Dialogue Manager. 
Button Onclick: переносим с иерархии текущий GameObject Say to NPC в онклик и выбираем  DialogueTrigger.TriggerDialogue;
шаг5. Вернуться в шаг 1 и заполнить поля.
