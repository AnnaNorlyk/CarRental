import { Router } from 'express';
import { publishMessage } from '../Redis/redisPublisher';

const router = Router();

router.post('/open', async (req, res) => {
  await publishMessage('cabinet-channel', 'open-cabinet');
  res.json({ message: 'Åbner skuffe sendt til Redis' });
});

router.post('/close', async (req, res) => {
  await publishMessage('cabinet-channel', 'close-cabinet');
  res.json({ message: 'Lukker skuffe sendt til Redis' });
});

export default router;
